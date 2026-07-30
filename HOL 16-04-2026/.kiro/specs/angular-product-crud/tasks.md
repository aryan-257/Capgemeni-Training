# Implementation Plan: Angular Product CRUD

## Overview

Implement a standalone Angular 17+ application for managing a product catalog. The implementation proceeds bottom-up: data model → service → component logic → template/styling → bootstrap wiring. Property-based tests (fast-check) and unit tests are included as optional sub-tasks alongside each implementation step.

## Tasks

- [x] 1. Create the Product data model
  - Create `src/app/models/product.model.ts` and export the `Product` interface with fields: `id` (number), `name` (string), `price` (number), `description` (string), `category` (string), `inStock` (boolean)
  - _Requirements: 1.1, 1.2_

- [x] 2. Implement ProductService
  - [x] 2.1 Scaffold the service file and BehaviorSubject store
    - Create `src/app/services/product.service.ts` with `@Injectable({ providedIn: 'root' })`
    - Declare a private `BehaviorSubject<Product[]>` initialised with the three sample products (Laptop, Coffee Mug, Notebook)
    - _Requirements: 2.1, 2.2_

  - [x] 2.2 Implement read methods
    - Implement `getProducts(): Observable<Product[]>` returning `this.productsSubject.asObservable()`
    - Implement `getProductById(id: number): Product | undefined` as a synchronous array find
    - _Requirements: 2.3, 2.4, 2.5_

  - [x] 2.3 Implement write methods (add, update, delete)
    - Implement `addProduct(product: Omit<Product, 'id'>): void` — assign a unique numeric id (max existing id + 1), push to array, call `next()`
    - Implement `updateProduct(product: Product): void` — replace by id, call `next()`
    - Implement `deleteProduct(id: number): void` — filter out by id, call `next()`
    - _Requirements: 2.6, 2.7, 2.8, 2.11_

  - [x] 2.4 Implement searchProducts
    - Implement `searchProducts(query: string): Product[]` — return all products when query is empty; otherwise filter by case-insensitive match on `name` or `category`
    - _Requirements: 2.9, 2.10_

  - [ ]* 2.5 Write property tests for ProductService
    - Install `fast-check` as a dev dependency if not already present
    - Create `src/app/services/product.service.spec.ts` (or equivalent PBT file)
    - **Property 1: Product round-trip identity** — generate arbitrary valid products, add, retrieve by assigned id, assert field equality
      - `// Feature: angular-product-crud, Property 1: Product round-trip identity`
      - **Validates: Requirements 2.6, 2.4**
    - **Property 2: Delete removes exactly one product** — generate arbitrary store states and a valid id, delete, assert size decreases by 1 and id absent
      - `// Feature: angular-product-crud, Property 2: Delete removes exactly one product`
      - **Validates: Requirements 2.8, 2.11**
    - **Property 3: Update preserves store size** — generate arbitrary store states and a valid id, update with new values, assert size unchanged and values updated
      - `// Feature: angular-product-crud, Property 3: Update preserves store size`
      - **Validates: Requirements 2.7, 2.11**
    - **Property 4: Search is a subset filter** — generate arbitrary non-empty queries, assert all results contain the query in name or category
      - `// Feature: angular-product-crud, Property 4: Search is a subset filter`
      - **Validates: Requirements 2.9**
    - **Property 5: Empty search returns all products** — generate arbitrary store states, assert `searchProducts('')` length equals store length
      - `// Feature: angular-product-crud, Property 5: Empty search returns all products`
      - **Validates: Requirements 2.10**
    - **Property 7: Unique id assignment on add** — generate arbitrary sequences of `addProduct` calls, assert all ids are unique
      - `// Feature: angular-product-crud, Property 7: Unique id assignment on add`
      - **Validates: Requirements 2.6**

  - [ ]* 2.6 Write unit tests for ProductService
    - Verify pre-loaded sample data contains exactly three products
    - Verify `getProductById` returns the correct product for a known id and `undefined` for an unknown id
    - Verify `addProduct` increases store length by one
    - Verify `updateProduct` changes the correct fields without affecting other products
    - Verify `deleteProduct` removes the correct product
    - Verify `searchProducts('')` returns all products and `searchProducts('laptop')` returns only matching products (case-insensitive)
    - _Requirements: 2.1–2.11_

- [x] 3. Checkpoint — service layer complete
  - Ensure all tests pass, ask the user if questions arise.

- [x] 4. Implement ProductListComponent — class logic
  - [x] 4.1 Scaffold the component and inject ProductService
    - Create `src/app/components/product-list/product-list.component.ts` as a standalone component with `standalone: true`, importing `CommonModule` and `FormsModule`
    - Declare all component properties: `products`, `filteredProducts`, `searchQuery`, `showModal`, `editingProduct`, `formData`, `showDeleteConfirm`, `productToDelete`, `formErrors`
    - Inject `ProductService` via constructor
    - _Requirements: 3.4, 10.1_

  - [x] 4.2 Implement subscription lifecycle
    - In `ngOnInit`, subscribe to `ProductService.getProducts()` and assign to `products`; call `onSearch()` after each emission to keep `filteredProducts` in sync
    - In `ngOnDestroy`, unsubscribe to prevent memory leaks
    - _Requirements: 3.2_

  - [x] 4.3 Implement search/filter logic
    - Implement `onSearch()`: when `searchQuery` is empty set `filteredProducts = products`; otherwise filter by case-insensitive match on `name` or `category`
    - _Requirements: 4.1, 4.2, 4.3_

  - [x] 4.4 Implement modal open/close methods
    - Implement `openAddModal()`: reset `formData` to empty object, set `editingProduct = null`, set `showModal = true`
    - Implement `openEditModal(product: Product)`: copy product fields into `formData`, set `editingProduct = product`, set `showModal = true`
    - Implement `closeModal()`: set `showModal = false`, clear `formData` and `formErrors`
    - _Requirements: 5.1, 5.2, 6.1, 6.2, 9.1, 9.2_

  - [x] 4.5 Implement form validation and submission
    - Implement `submitForm()`: collect validation errors for empty/whitespace `name`, `price`, `category`, `description` and non-positive `price`; if errors exist populate `formErrors` and return without calling the service; if valid call `addProduct` or `updateProduct` then `closeModal()`
    - _Requirements: 5.3, 5.4, 5.5, 6.3, 6.4, 9.3_

  - [x] 4.6 Implement delete confirmation flow
    - Implement `confirmDelete(id: number)`: set `productToDelete = id`, set `showDeleteConfirm = true`
    - Implement `executeDelete()`: call `ProductService.deleteProduct(productToDelete)`, clear `productToDelete`, set `showDeleteConfirm = false`
    - Implement `cancelDelete()`: clear `productToDelete`, set `showDeleteConfirm = false`
    - _Requirements: 7.1, 7.2, 7.3, 7.4_

  - [x] 4.7 Implement getStatistics()
    - Implement `getStatistics()` returning `{ total, inStock, outOfStock, averagePrice }` computed from `products`; `averagePrice` rounded to 2 decimal places; return zeros when `products` is empty
    - _Requirements: 8.1, 8.2, 8.3, 8.4, 8.5_

  - [ ]* 4.8 Write property tests for component logic
    - **Property 6: Statistics are consistent with store** — generate arbitrary product arrays, pass to `getStatistics()`, assert `total = inStock + outOfStock` and `averagePrice = sum(prices)/total` (rounded to 2 dp)
      - `// Feature: angular-product-crud, Property 6: Statistics are consistent with store`
      - **Validates: Requirements 8.1, 8.2, 8.3, 8.4**
    - **Property 8: Whitespace-only form fields are invalid** — generate whitespace-only strings for required fields, assert `submitForm()` populates `formErrors` and does not call the service
      - `// Feature: angular-product-crud, Property 8: Whitespace-only form fields are invalid`
      - **Validates: Requirements 5.3, 5.5, 6.4**

  - [ ]* 4.9 Write unit tests for ProductListComponent
    - Test that the component renders a row for each product
    - Test that the "no products" message is shown when the list is empty
    - Test that `openAddModal()` sets `editingProduct = null` and `showModal = true`
    - Test that `openEditModal(product)` pre-fills `formData` and sets `showModal = true`
    - Test that `executeDelete()` calls `ProductService.deleteProduct` with the correct id
    - Test that `cancelDelete()` does not call `ProductService.deleteProduct`
    - Test that `getStatistics()` returns correct values for a known product array
    - _Requirements: 3.1, 3.3, 5.1, 5.2, 6.1, 6.2, 7.1–7.4, 8.1–8.5_

- [x] 5. Checkpoint — component logic complete
  - Ensure all tests pass, ask the user if questions arise.

- [x] 6. Build the ProductListComponent template and styles
  - [x] 6.1 Create the HTML template
    - Create `src/app/components/product-list/product-list.component.html`
    - Add a search input bound to `searchQuery` with `(input)="onSearch()"` — _Requirements: 4.1_
    - Add an "Add Product" button calling `openAddModal()` — _Requirements: 5.1_
    - Add the statistics section displaying total, inStock, outOfStock, and averagePrice from `getStatistics()` — _Requirements: 8.1–8.5_
    - Add the product table with columns: Name, Price, Description, Category, In Stock, Actions; iterate over `filteredProducts` with `*ngFor`; show "no products" message when `filteredProducts` is empty — _Requirements: 3.1, 3.3_
    - Add "Edit" and "Delete" action buttons per row calling `openEditModal(product)` and `confirmDelete(product.id)` — _Requirements: 6.1, 7.1_
    - Add the modal overlay (controlled by `showModal`) containing the product form with fields for Name, Price, Description, Category, In Stock; display `formErrors`; include Submit and Cancel buttons — _Requirements: 5.2, 5.3, 5.5, 6.2, 9.1, 9.2_
    - Add the delete confirmation dialog (controlled by `showDeleteConfirm`) with Confirm and Cancel buttons — _Requirements: 7.2, 7.3, 7.4_

  - [x] 6.2 Create the component stylesheet
    - Create `src/app/components/product-list/product-list.component.css`
    - Style the product table with horizontal scroll on viewports narrower than 768px — _Requirements: 11.1_
    - Style the modal to be centered and constrained to a minimum viewport width of 320px — _Requirements: 11.2_
    - Style the statistics section as a responsive grid that stacks vertically on narrow viewports — _Requirements: 11.3_
    - Style the modal overlay to block interaction with the table behind it — _Requirements: 9.1_

- [x] 7. Bootstrap the application
  - [x] 7.1 Create AppComponent
    - Create `src/app/app.component.ts` as a standalone component that imports `ProductListComponent` and renders `<app-product-list>` in its template
    - _Requirements: 10.1_

  - [x] 7.2 Create app configuration and routes
    - Create `src/app/app.config.ts` exporting an `ApplicationConfig` with the necessary Angular providers
    - Create `src/app/app.routes.ts` exporting an empty `Routes` array
    - _Requirements: 10.3, 10.5_

  - [x] 7.3 Create main.ts and index.html
    - Create `src/main.ts` calling `bootstrapApplication(AppComponent, appConfig)`
    - Create `src/index.html` with the `<app-root>` selector
    - _Requirements: 10.2, 10.4_

  - [ ]* 7.4 Write integration/smoke tests
    - Write a smoke test verifying the application bootstraps without errors
    - Write an integration test verifying `ProductListComponent` renders inside `AppComponent`
    - _Requirements: 10.1, 10.2_

- [x] 8. Final checkpoint — Ensure all tests pass
  - Ensure all tests pass, ask the user if questions arise.

## Notes

- Tasks marked with `*` are optional and can be skipped for a faster MVP
- Each task references specific requirements for traceability
- Property tests use [fast-check](https://github.com/dubzzz/fast-check) and should run a minimum of 100 iterations per property
- Unit tests and property tests are complementary — both should be present for full coverage
- Checkpoints ensure incremental validation at the service layer, component logic layer, and final integration
