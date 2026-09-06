# StockManager Pro

Application de gestion de stock pour PME — API REST en ASP.NET Core 8 consommée par un frontend HTML/CSS/JS.

---

# Objectif

Permettre à une petite entreprise de gérer ses produits, ses fournisseurs, ses catégories et ses mouvements de stock (entrées/sorties) avec alertes automatiques en cas de stock insuffisant.

---

# Stack technique

| Couche | Technologie |
|--------|-------------|
| Backend | C# / ASP.NET Core 8 |
| Base de données | SQL Server LocalDB + Entity Framework Core 8 |
| Authentification | JWT + BCrypt |
| Frontend | HTML / CSS / JavaScript (Fetch API) |

---

# Architecture

Frontend (HTML/CSS/JS)
↓ HTTP + JWT
Controllers (API REST)
↓
Services (logique métier)
↓
AppDbContext (Entity Framework)
↓
SQL Server


## Couche Service
Chaque entité dispose d'une interface et d'une implémentation :
- `ICategorieService` / `CategorieService`
- `IFournisseurService` / `FournisseurService`
- `IProduitService` / `ProduitService`
- `IMouvementService` / `MouvementService`
- `IDashboardService` / `DashboardService`
- `IAuthService` / `AuthService`

---

# Structure du projet

StockManagerPro/
├── StockManagerPro.API/
│ ├── Controllers/
│ ├── Services/
│ ├── Models/
│ ├── DTOs/
│ ├── Data/
│ └── Frontend/
│ ├── login.html
│ ├── dashboard.html
│ ├── produits.html
│ ├── mouvements.html
│ └── alertes.html


# Endpoints API

# Auth
| Méthode | Route | Description |
|---------|-------|-------------|
| POST | `/api/Auth/register` | Créer un compte |
| POST | `/api/Auth/login` | Connexion → retourne un token JWT |

# Produits
| Méthode | Route | Description |
|---------|-------|-------------|
| GET | `/api/Produits` | Liste tous les produits |
| GET | `/api/Produits/{id}` | Détail d'un produit |
| GET | `/api/Produits/alertes` | Produits sous le seuil d'alerte |
| POST | `/api/Produits` | Créer un produit |
| PUT | `/api/Produits/{id}` | Modifier un produit |
| DELETE | `/api/Produits/{id}` | Supprimer un produit |

# Mouvements
| Méthode | Route | Description |
|---------|-------|-------------|
| GET | `/api/Mouvements` | Historique des mouvements |
| GET | `/api/Mouvements/produit/{id}` | Mouvements d'un produit |
| POST | `/api/Mouvements/entree` | Entrée de stock |
| POST | `/api/Mouvements/sortie` | Sortie de stock (vérifie le stock disponible) |

# Catégories & Fournisseurs
| Méthode | Route | Description |
|---------|-------|-------------|
| GET/POST/PUT/DELETE | `/api/Categories` | CRUD catégories |
| GET/POST/PUT/DELETE | `/api/Fournisseurs` | CRUD fournisseurs |
| GET | `/api/Fournisseurs/{id}/produits` | Produits d'un fournisseur |
| GET | `/api/Dashboard` | Statistiques globales |

---

## 🚀 Lancer le projet

# Prérequis
- .NET 8 SDK
- SQL Server LocalDB
- Python 3 (pour le serveur frontend)

# Backend
```bash
cd StockManagerPro.API
dotnet run --environment Development
```

L'API tourne sur `http://localhost:5000`.
Swagger disponible sur `http://localhost:5000/swagger`.

# Frontend
```bash
cd StockManagerPro.API/Frontend
python -m http.server 3000
```

Ouvrir `http://localhost:3000/login.html` dans le navigateur.

# Base de données
Les migrations sont déjà incluses. Au premier lancement, EF Core crée automatiquement la base.

---

# Sécurité

- Authentification JWT Bearer sur tous les endpoints sauf `/api/Auth`
- Mots de passe hashés avec BCrypt
- Token valide 24h

---

#Auteur

Développé par **Nga Abolo S.H** dans le cadre d'un projet portfolio.