# Student Management System

A complete ASP.NET Core MVC Web Application for managing students, courses, and departments using Entity Framework Core Code First approach.

## Features

### Authentication System
- Custom authentication (no ASP.NET Identity)
- User registration with role selection (Teacher/Student)
- Login with email and password
- Role-based dashboard redirection

### Teacher Dashboard
- View system statistics (total departments, courses, students)
- Manage Departments (CRUD operations)
- Manage Courses (CRUD operations with department linking)
- Manage Students (CRUD operations with search and filtering)
- View students per department

### Student Dashboard
- View personal profile and course details
- Update profile (phone number and address only)
- View course information (name, duration, fees, department)

### Database Design
- **User**: UserId, FullName, Email, Password, Role
- **Department**: DepartmentId, DepartmentName, Description
- **Course**: CourseId, CourseName, Duration, Fees, DepartmentId
- **Student**: StudentId, StudentName, Email, PhoneNumber, Address, DepartmentId, CourseId

## Technology Stack
- ASP.NET Core MVC (.NET 10.0)
- Entity Framework Core (Code First)
- SQL Server LocalDB
- Bootstrap 5 for responsive UI
- jQuery for client-side interactions

## Getting Started

### Prerequisites
- .NET 10.0 SDK
- SQL Server LocalDB (comes with Visual Studio)

### Installation & Setup

1. **Clone or download the project**

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Update database** (if not already done)
   ```bash
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Access the application**
   - Open browser and navigate to `https://localhost:5001` or `http://localhost:5000`
   - You'll be redirected to the login page

### Default Data
The application includes seed data:
- **Departments**: Computer Science, Business Administration
- **Courses**: Bachelor of Computer Science, Master of Computer Applications, Bachelor of Business Administration

### Usage

1. **Register a new account**
   - Click "Register here" on login page
   - Fill in basic details and select role (Teacher or Student)
   - **For Students**: Additional fields will appear for phone, address, department, and course selection
   - After registration, you'll be redirected to login

2. **Login as Teacher**
   - Access full dashboard with management capabilities
   - Manage departments, courses, and students
   - View system statistics
   - **New**: See users who registered as students but need profile completion

3. **Login as Student**
   - View personal dashboard with profile and course information
   - Update profile information (phone and address)
   - **New**: Automatic profile creation during registration

### Dynamic Integration Features

#### Enhanced Registration Process
- **Student Registration**: When registering as a student, users must provide:
  - Basic info (name, email, password)
  - Contact details (phone, address)
  - Academic info (department, course selection)
- **Automatic Profile Creation**: Student records are automatically created upon registration
- **Cascading Dropdowns**: Course selection updates based on department choice

#### Teacher Portal Integration
- **Student Management**: Teachers can see all registered students
- **Profile Completion**: Teachers can create profiles for users who registered as students but lack complete records
- **Unified View**: All student data is accessible and manageable from the teacher portal

#### Student Portal Integration
- **Immediate Access**: Students can access their dashboard immediately after registration
- **Profile Management**: Students can update their contact information
- **Course Information**: Full access to enrolled course details

### Key Features

#### Teacher Capabilities
- **Department Management**: Add, edit, delete departments
- **Course Management**: Add, edit, delete courses (linked to departments)
- **Student Management**: Add, edit, delete students with search and filter
- **Dashboard**: View statistics and quick access to management functions

#### Student Capabilities
- **Profile View**: See personal information and course details
- **Profile Update**: Edit phone number and address (other fields read-only)
- **Course Information**: View enrolled course details

#### Additional Features
- **Search Students**: Search by name
- **Filter Students**: Filter by department
- **Responsive Design**: Bootstrap-based responsive UI
- **Form Validation**: Client and server-side validation
- **Confirmation Dialogs**: For delete operations
- **Success/Error Messages**: User feedback for operations

## Project Structure

```
StudentManagementSystem/
├── Controllers/
│   ├── AccountController.cs
│   ├── CourseController.cs
│   ├── DepartmentController.cs
│   ├── StudentController.cs
│   ├── StudentDashboardController.cs
│   └── TeacherDashboardController.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Models/
│   ├── Course.cs
│   ├── Department.cs
│   ├── Student.cs
│   └── User.cs
├── ViewModels/
│   ├── LoginViewModel.cs
│   ├── RegisterViewModel.cs
│   └── StudentProfileViewModel.cs
├── Views/
│   ├── Account/
│   ├── Course/
│   ├── Department/
│   ├── Student/
│   ├── StudentDashboard/
│   ├── TeacherDashboard/
│   └── Shared/
└── wwwroot/
    ├── css/
    ├── js/
    └── lib/
```

## Database Connection

The application uses SQL Server LocalDB with the connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StudentManagementSystemDb;Trusted_Connection=true;MultipleActiveResultSets=true"
}
```

## Security Features
- Password hashing using SHA256
- Session-based authentication
- Role-based access control
- CSRF protection with anti-forgery tokens
- Input validation and sanitization

## Future Enhancements
- Email verification for registration
- Password reset functionality
- File upload for student photos
- Advanced reporting features
- Export functionality (PDF, Excel)
- Audit logging
- Advanced search and filtering options