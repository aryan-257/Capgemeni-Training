"""
API Testing Script
Tests the Flask API endpoints
"""

import requests
import json
from datetime import datetime, timedelta
import time

API_URL = "http://localhost:5000"


def print_section(title):
    """Print a formatted section header"""
    print("\n" + "="*70)
    print(f"  {title}")
    print("="*70)


def test_home():
    """Test home endpoint"""
    print_section("TEST 1: Home Endpoint (GET /)")
    
    try:
        response = requests.get(f"{API_URL}/")
        print(f"Status Code: {response.status_code}")
        print(f"Response:\n{json.dumps(response.json(), indent=2)}")
        return response.status_code == 200
    except Exception as e:
        print(f"❌ Error: {e}")
        return False


def test_health():
    """Test health check endpoint"""
    print_section("TEST 2: Health Check (GET /health)")
    
    try:
        response = requests.get(f"{API_URL}/health")
        print(f"Status Code: {response.status_code}")
        print(f"Response:\n{json.dumps(response.json(), indent=2)}")
        return response.status_code == 200
    except Exception as e:
        print(f"❌ Error: {e}")
        return False


def test_models_info():
    """Test models info endpoint"""
    print_section("TEST 3: Models Info (GET /models)")
    
    try:
        response = requests.get(f"{API_URL}/models")
        print(f"Status Code: {response.status_code}")
        print(f"Response:\n{json.dumps(response.json(), indent=2)}")
        return response.status_code == 200
    except Exception as e:
        print(f"❌ Error: {e}")
        return False


def test_single_prediction(metal="gold"):
    """Test single prediction"""
    print_section(f"TEST 4: Single Prediction (POST /predict) - {metal.upper()}")
    
    try:
        tomorrow = (datetime.now() + timedelta(days=1)).strftime('%Y-%m-%d')
        
        payload = {
            "metal": metal,
            "predictionDate": tomorrow,
            "daysAhead": 1
        }
        
        print(f"Request Payload:\n{json.dumps(payload, indent=2)}")
        
        response = requests.post(
            f"{API_URL}/predict",
            json=payload,
            headers={'Content-Type': 'application/json'}
        )
        
        print(f"\nStatus Code: {response.status_code}")
        print(f"Response:\n{json.dumps(response.json(), indent=2)}")
        
        if response.status_code == 200:
            result = response.json()
            print(f"\n✓ Predicted {metal.upper()} price: ${result['predictedPrice']:.2f}")
            print(f"✓ Confidence: {result['confidence']:.2%}")
        
        return response.status_code == 200
        
    except Exception as e:
        print(f"❌ Error: {e}")
        return False


def test_batch_prediction(metal="gold", days=7):
    """Test batch prediction"""
    print_section(f"TEST 5: Batch Prediction (POST /predict/batch) - {metal.upper()}, {days} days")
    
    try:
        payload = {
            "metal": metal,
            "daysAhead": days
        }
        
        print(f"Request Payload:\n{json.dumps(payload, indent=2)}")
        
        response = requests.post(
            f"{API_URL}/predict/batch",
            json=payload,
            headers={'Content-Type': 'application/json'}
        )
        
        print(f"\nStatus Code: {response.status_code}")
        
        if response.status_code == 200:
            result = response.json()
            print(f"Total Predictions: {result['total']}")
            print(f"\nPredictions:")
            print(f"{'Day':<6} {'Date':<12} {'Price':<12} {'Confidence':<12}")
            print("-" * 50)
            
            for pred in result['predictions']:
                print(f"{pred['daysAhead']:<6} {pred['predictionDate']:<12} "
                      f"${pred['predictedPrice']:<11.2f} {pred['confidence']:<11.2%}")
        else:
            print(f"Response:\n{json.dumps(response.json(), indent=2)}")
        
        return response.status_code == 200
        
    except Exception as e:
        print(f"❌ Error: {e}")
        return False


def test_error_handling():
    """Test error handling"""
    print_section("TEST 6: Error Handling")
    
    tests = [
        {
            'name': 'Invalid metal',
            'payload': {'metal': 'platinum', 'predictionDate': '2024-12-15'},
            'expected_status': 404
        },
        {
            'name': 'Missing metal',
            'payload': {'predictionDate': '2024-12-15'},
            'expected_status': 400
        },
        {
            'name': 'Invalid days ahead',
            'payload': {'metal': 'gold', 'daysAhead': 100},
            'expected_status': 400
        }
    ]
    
    all_passed = True
    
    for test in tests:
        print(f"\n  Testing: {test['name']}")
        try:
            response = requests.post(
                f"{API_URL}/predict",
                json=test['payload'],
                headers={'Content-Type': 'application/json'}
            )
            
            if response.status_code == test['expected_status']:
                print(f"  ✓ Correct status code: {response.status_code}")
                print(f"  Error message: {response.json().get('errorMessage')}")
            else:
                print(f"  ❌ Expected {test['expected_status']}, got {response.status_code}")
                all_passed = False
                
        except Exception as e:
            print(f"  ❌ Error: {e}")
            all_passed = False
    
    return all_passed


def test_performance():
    """Test API performance"""
    print_section("TEST 7: Performance Test")
    
    try:
        num_requests = 10
        print(f"Making {num_requests} prediction requests...")
        
        start_time = time.time()
        
        for i in range(num_requests):
            payload = {
                "metal": "gold",
                "predictionDate": "2024-12-15",
                "daysAhead": 1
            }
            response = requests.post(f"{API_URL}/predict", json=payload)
            
            if response.status_code != 200:
                print(f"❌ Request {i+1} failed")
                return False
        
        end_time = time.time()
        total_time = end_time - start_time
        avg_time = total_time / num_requests
        
        print(f"\n✓ All {num_requests} requests successful")
        print(f"Total time: {total_time:.2f} seconds")
        print(f"Average time per request: {avg_time*1000:.2f} ms")
        print(f"Requests per second: {num_requests/total_time:.2f}")
        
        return True
        
    except Exception as e:
        print(f"❌ Error: {e}")
        return False


def main():
    """Run all tests"""
    print("="*70)
    print("  FLASK API TESTING SUITE")
    print("="*70)
    print("\nMake sure the API is running:")
    print("  python api.py")
    print("\nWaiting for API to be ready...")
    
    # Wait for API to be ready
    max_retries = 5
    for i in range(max_retries):
        try:
            response = requests.get(f"{API_URL}/health", timeout=2)
            if response.status_code == 200:
                print("✓ API is ready!\n")
                break
        except:
            if i < max_retries - 1:
                print(f"  Waiting... ({i+1}/{max_retries})")
                time.sleep(2)
            else:
                print("\n❌ API is not responding. Please start it with: python api.py")
                return
    
    # Run tests
    results = {
        'Home Endpoint': test_home(),
        'Health Check': test_health(),
        'Models Info': test_models_info(),
        'Gold Prediction': test_single_prediction('gold'),
        'Silver Prediction': test_single_prediction('silver'),
        'Batch Prediction': test_batch_prediction('gold', 7),
        'Error Handling': test_error_handling(),
        'Performance': test_performance()
    }
    
    # Print summary
    print_section("TEST SUMMARY")
    
    passed = sum(1 for result in results.values() if result)
    total = len(results)
    
    for test_name, result in results.items():
        status = "✓ PASS" if result else "❌ FAIL"
        print(f"{test_name:<25} {status}")
    
    print("\n" + "="*70)
    print(f"Results: {passed}/{total} tests passed")
    
    if passed == total:
        print("✓ All tests passed! API is working correctly.")
    else:
        print("⚠ Some tests failed. Check the output above for details.")
    
    print("="*70)


if __name__ == "__main__":
    main()
