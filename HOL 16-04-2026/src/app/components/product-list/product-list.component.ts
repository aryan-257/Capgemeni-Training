import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
})
export class ProductListComponent implements OnInit, OnDestroy {
  products: Product[] = [];
  filteredProducts: Product[] = [];
  searchQuery: string = '';
  showModal: boolean = false;
  editingProduct: Product | null = null;
  formData: Partial<Product> = {};
  showDeleteConfirm: boolean = false;
  productToDelete: number | null = null;
  formErrors: string[] = [];

  private subscription: Subscription | null = null;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.subscription = this.productService.getProducts().subscribe((products) => {
      this.products = products;
      this.onSearch();
    });
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  onSearch(): void {
    if (!this.searchQuery) {
      this.filteredProducts = this.products;
    } else {
      const query = this.searchQuery.toLowerCase();
      this.filteredProducts = this.products.filter(
        (p) =>
          p.name.toLowerCase().includes(query) ||
          p.category.toLowerCase().includes(query)
      );
    }
  }

  openAddModal(): void {
    this.formData = {};
    this.editingProduct = null;
    this.showModal = true;
    this.formErrors = [];
  }

  openEditModal(product: Product): void {
    this.formData = { ...product };
    this.editingProduct = product;
    this.showModal = true;
    this.formErrors = [];
  }

  closeModal(): void {
    this.showModal = false;
    this.formData = {};
    this.formErrors = [];
    this.editingProduct = null;
  }

  submitForm(): void {
    this.formErrors = [];

    const name = this.formData.name?.trim() ?? '';
    const category = this.formData.category?.trim() ?? '';
    const description = this.formData.description?.trim() ?? '';
    const price = this.formData.price;

    if (!name) {
      this.formErrors.push('Name is required.');
    }
    if (price === undefined || price === null || price <= 0) {
      this.formErrors.push('Price must be a positive number.');
    }
    if (!category) {
      this.formErrors.push('Category is required.');
    }
    if (!description) {
      this.formErrors.push('Description is required.');
    }

    if (this.formErrors.length > 0) {
      return;
    }

    const validData: Omit<Product, 'id'> = {
      name: name,
      price: price as number,
      category: category,
      description: description,
      inStock: this.formData.inStock ?? false,
    };

    if (this.editingProduct !== null) {
      this.productService.updateProduct({ ...validData, id: this.editingProduct.id });
    } else {
      this.productService.addProduct(validData);
    }

    this.closeModal();
  }

  confirmDelete(id: number): void {
    this.productToDelete = id;
    this.showDeleteConfirm = true;
  }

  executeDelete(): void {
    this.productService.deleteProduct(this.productToDelete!);
    this.productToDelete = null;
    this.showDeleteConfirm = false;
  }

  cancelDelete(): void {
    this.productToDelete = null;
    this.showDeleteConfirm = false;
  }

  getStatistics(): { total: number; inStock: number; outOfStock: number; averagePrice: number } {
    const total = this.products.length;
    if (total === 0) {
      return { total: 0, inStock: 0, outOfStock: 0, averagePrice: 0 };
    }
    const inStock = this.products.filter((p) => p.inStock === true).length;
    const outOfStock = this.products.filter((p) => p.inStock === false).length;
    const averagePrice = parseFloat((this.products.reduce((sum, p) => sum + p.price, 0) / total).toFixed(2));
    return { total, inStock, outOfStock, averagePrice };
  }
}
