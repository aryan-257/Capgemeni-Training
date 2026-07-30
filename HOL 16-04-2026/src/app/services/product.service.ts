import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Product } from '../models/product.model';

const SAMPLE_DATA: Product[] = [
  {
    id: 1,
    name: 'Laptop',
    price: 999.99,
    description: 'High-performance laptop with 16GB RAM',
    category: 'Electronics',
    inStock: true,
  },
  {
    id: 2,
    name: 'Coffee Mug',
    price: 15.99,
    description: 'Ceramic coffee mug, 350ml',
    category: 'Kitchen',
    inStock: true,
  },
  {
    id: 3,
    name: 'Notebook',
    price: 5.99,
    description: '100-page ruled notebook',
    category: 'Stationery',
    inStock: false,
  },
];

@Injectable({ providedIn: 'root' })
export class ProductService {
  private productsSubject = new BehaviorSubject<Product[]>(SAMPLE_DATA);

  getProducts(): Observable<Product[]> {
    return this.productsSubject.asObservable();
  }

  getProductById(id: number): Product | undefined {
    return this.productsSubject.getValue().find((p) => p.id === id);
  }

  addProduct(product: Omit<Product, 'id'>): void {
    const current = this.productsSubject.getValue();
    const newId = current.length > 0 ? Math.max(...current.map((p) => p.id)) + 1 : 1;
    const newProduct: Product = { id: newId, ...product };
    this.productsSubject.next([...current, newProduct]);
  }

  updateProduct(product: Product): void {
    const current = this.productsSubject.getValue();
    const updated = current.map((p) => (p.id === product.id ? product : p));
    this.productsSubject.next(updated);
  }

  deleteProduct(id: number): void {
    const current = this.productsSubject.getValue();
    this.productsSubject.next(current.filter((p) => p.id !== id));
  }

  searchProducts(query: string): Product[] {
    const current = this.productsSubject.getValue();
    if (!query) {
      return current;
    }
    const lower = query.toLowerCase();
    return current.filter(
      (p) =>
        p.name.toLowerCase().includes(lower) ||
        p.category.toLowerCase().includes(lower)
    );
  }
}
