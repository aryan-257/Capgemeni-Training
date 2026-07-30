# Requirements Document

## Introduction

This feature is a complete Angular CRUD application for managing Products. It provides a standalone Angular application with a reactive in-memory data store, a product list view with search and filtering, modal forms for creating and editing products, delete confirmation, and a statistics summary. The application uses Angular standalone components, reactive programming via RxJS BehaviorSubject, and responsive CSS styling.

## Glossary

- **Product**: An entity with an id, name, price, description, category, and inStock flag
- **Product_Service**: The Angular service responsible for managing the in-memory product data store
- **Product_List_Component**: The Angular standalone component that renders the product table, search bar, statistics, and modal form
- **App_Component**: The root Angular standalone component that bootstraps the application
- **Modal_Form**: An overlay dialog used to create or edit a Product
- **BehaviorSubject**: An RxJS reactive data structure that holds the current product list and emits updates to subscribers
- **Sample_Data**: The initial set of three pre-loaded products (Laptop, Coffee Mug, Notebook)
- **Statistics_Section**: A summary panel displaying total products, in-stock count, out-of-stock count, and average price

---

## Requirements

### Requirement 1: Product Data Model

**User Story:** As a developer, I want a strongly-typed Product interface, so that all parts of the application share a consistent data shape.

#### Acceptance Criteria

1. THE Product_Model SHALL define a `Product` interface with fields: `id` (number), `name` (string), `price` (number), `description` (string), `category` (string), and `inStock` (boolean).
2. THE Product_Model SHALL be exported from `src/app/models/product.model.ts`.

---

### Requirement 2: Reactive Product Data Store

**User Story:** As a developer, I want a service that manages products reactively, so that all components automatically reflect data changes without manual refresh.

#### Acceptance Criteria

1. THE Product_Service SHALL maintain an in-memory array of products backed by a BehaviorSubject.
2. THE Product_Service SHALL pre-load Sample_Data containing at least three products: Laptop, Coffee Mug, and Notebook.
3. WHEN `getProducts()` is called, THE Product_Service SHALL return an Observable that emits the current product list and all subsequent updates.
4. WHEN `getProductById(id)` is called with a valid id, THE Product_Service SHALL return the matching Product.
5. IF `getProductById(id)` is called with an id that does not exist, THEN THE Product_Service SHALL return `undefined`.
6. WHEN `addProduct(product)` is called, THE Product_Service SHALL assign a unique numeric id to the new product and add it to the store.
7. WHEN `updateProduct(product)` is called with an existing product id, THE Product_Service SHALL replace the stored product with the updated values.
8. WHEN `deleteProduct(id)` is called with an existing id, THE Product_Service SHALL remove the product with that id from the store.
9. WHEN `searchProducts(query)` is called with a non-empty string, THE Product_Service SHALL return products whose `name` or `category` contains the query string (case-insensitive).
10. WHEN `searchProducts(query)` is called with an empty string, THE Product_Service SHALL return all products.
11. AFTER any add, update, or delete operation, THE Product_Service SHALL emit the updated product list through the BehaviorSubject so all subscribers receive the change.

---

### Requirement 3: Product List View

**User Story:** As a user, I want to see all products in a table, so that I can review the current product inventory at a glance.

#### Acceptance Criteria

1. THE Product_List_Component SHALL display all products in an HTML table with columns: Name, Price, Description, Category, In Stock, and Actions.
2. THE Product_List_Component SHALL subscribe to the Product_Service observable and re-render the table whenever the product list changes.
3. WHEN the product list is empty, THE Product_List_Component SHALL display a message indicating no products are available.
4. THE Product_List_Component SHALL be implemented as a standalone Angular component located at `src/app/components/product-list/product-list.component.ts`.

---

### Requirement 4: Search and Filter

**User Story:** As a user, I want to search products by name or category, so that I can quickly find specific items in a large inventory.

#### Acceptance Criteria

1. THE Product_List_Component SHALL provide a text input field for entering a search query.
2. WHEN the user types in the search field, THE Product_List_Component SHALL filter the displayed products to those whose `name` or `category` contains the entered text (case-insensitive).
3. WHEN the search field is cleared, THE Product_List_Component SHALL display all products.

---

### Requirement 5: Create Product

**User Story:** As a user, I want to add a new product using a form, so that I can expand the product catalog.

#### Acceptance Criteria

1. THE Product_List_Component SHALL provide an "Add Product" button that opens the Modal_Form.
2. WHEN the Modal_Form is opened for creation, THE Product_List_Component SHALL present an empty form with fields: Name, Price, Description, Category, and In Stock.
3. THE Modal_Form SHALL require Name, Price, Category, and Description fields to be non-empty before submission is allowed.
4. WHEN the user submits a valid creation form, THE Product_List_Component SHALL call `addProduct()` on the Product_Service and close the Modal_Form.
5. IF the user submits the form with missing required fields, THEN THE Modal_Form SHALL display a validation error message and SHALL NOT submit the product.

---

### Requirement 6: Edit Product

**User Story:** As a user, I want to edit an existing product, so that I can correct or update product information.

#### Acceptance Criteria

1. THE Product_List_Component SHALL provide an "Edit" action button for each product row in the table.
2. WHEN the user clicks "Edit" for a product, THE Product_List_Component SHALL open the Modal_Form pre-filled with that product's current values.
3. WHEN the user submits a valid edit form, THE Product_List_Component SHALL call `updateProduct()` on the Product_Service and close the Modal_Form.
4. IF the user submits the edit form with missing required fields, THEN THE Modal_Form SHALL display a validation error message and SHALL NOT submit the update.

---

### Requirement 7: Delete Product

**User Story:** As a user, I want to delete a product with a confirmation step, so that I do not accidentally remove items from the catalog.

#### Acceptance Criteria

1. THE Product_List_Component SHALL provide a "Delete" action button for each product row in the table.
2. WHEN the user clicks "Delete" for a product, THE Product_List_Component SHALL display a confirmation dialog asking the user to confirm the deletion.
3. WHEN the user confirms deletion, THE Product_List_Component SHALL call `deleteProduct(id)` on the Product_Service.
4. WHEN the user cancels the confirmation dialog, THE Product_List_Component SHALL take no action and the product SHALL remain in the store.

---

### Requirement 8: Statistics Section

**User Story:** As a user, I want to see a summary of product statistics, so that I can understand the overall state of the inventory at a glance.

#### Acceptance Criteria

1. THE Statistics_Section SHALL display the total number of products currently in the store.
2. THE Statistics_Section SHALL display the count of products where `inStock` is `true`.
3. THE Statistics_Section SHALL display the count of products where `inStock` is `false`.
4. THE Statistics_Section SHALL display the average price of all products, rounded to two decimal places.
5. WHEN the product list changes, THE Statistics_Section SHALL update all displayed values to reflect the current state of the store.

---

### Requirement 9: Modal Form Behavior

**User Story:** As a user, I want the modal form to open and close cleanly, so that I can manage products without navigating away from the list.

#### Acceptance Criteria

1. WHEN the Modal_Form is open, THE Product_List_Component SHALL prevent interaction with the product table behind the modal overlay.
2. WHEN the user clicks "Cancel" or closes the Modal_Form without submitting, THE Product_List_Component SHALL discard any entered values and close the modal.
3. WHEN the Modal_Form is submitted successfully, THE Product_List_Component SHALL close the modal and the updated product list SHALL be visible immediately.

---

### Requirement 10: Application Bootstrap and Configuration

**User Story:** As a developer, I want the application to bootstrap correctly with standalone components and no routing, so that the app starts with the product list as the sole view.

#### Acceptance Criteria

1. THE App_Component SHALL be a standalone Angular component that imports and renders the Product_List_Component.
2. THE App_Component SHALL be bootstrapped via `src/main.ts` using `bootstrapApplication`.
3. THE application configuration in `src/app/app.config.ts` SHALL provide the necessary Angular providers for the standalone application.
4. THE `src/index.html` SHALL reference the `<app-root>` selector to mount the App_Component.
5. THE `src/app/app.routes.ts` SHALL export an empty routes array, as routing is not required for this application.

---

### Requirement 11: Responsive Styling

**User Story:** As a user, I want the application to be usable on different screen sizes, so that I can manage products from desktop or mobile devices.

#### Acceptance Criteria

1. THE Product_List_Component SHALL apply CSS styles that make the product table horizontally scrollable on viewports narrower than 768px.
2. THE Modal_Form SHALL be centered on the screen and SHALL NOT overflow the viewport on screens as small as 320px wide.
3. THE Statistics_Section SHALL display its summary cards in a responsive grid that stacks vertically on narrow viewports.
