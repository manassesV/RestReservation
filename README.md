
markdown
Copiar
Editar
# 🍽️ Restaurant Reservation System

A simple web-based reservation system built using **ASP.NET Core MVC** and **MongoDB**. It allows users to view, add, edit, and delete restaurant reservations.

---

## 🔧 Tech Stack

- **Backend**: ASP.NET Core MVC
- **Database**: MongoDB
- **Frontend**: Razor Views (HTML, Bootstrap optionally)
- **ORM / Driver**: MongoDB .NET Driver

---

## 📁 Project Structure

RestReservation/ ├── Controllers/ │ └── ReservationController.cs ├── Models/ │ └── Reservation.cs ├── Services/ │ ├── IReservationService.cs │ └── ReservationService.cs ├── ViewModels/ │ ├── ReservationAddViewModel.cs │ └── ReservationListViewModel.cs ├── Views/ │ └── Reservation/ │ ├── Index.cshtml │ ├── Add.cshtml │ ├── Edit.cshtml │ └── Delete.cshtml └── Program.cs / Startup.cs

yaml
Copiar
Editar

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/your-repo-name.git
cd your-repo-name
2. Set Up MongoDB
Make sure MongoDB is running locally (or set a remote URI).
Set the connection string in your appsettings.json:

json
Copiar
Editar
"MongoDbSettings": {
  "ConnectionString": "mongodb://localhost:27017",
  "DatabaseName": "RestaurantReservation"
}
3. Run the Project
bash
Copiar
Editar
dotnet run
Visit http://localhost:5000/Reservation in your browser.

✨ Features
View all reservations

Create new reservation for a restaurant

Edit existing reservation

Delete reservation with confirmation

MongoDB backend with ObjectId handling

📸 Screenshots
Coming soon: UI previews

🛠️ TODO
Add user authentication

Prevent double bookings

Add pagination and filtering

REST API support

📄 License
MIT License — feel free to use and adapt!

yaml
Copiar
Editar

---

Let me know if you'd like to add a usage example, deployment guide, or sample data seedin
