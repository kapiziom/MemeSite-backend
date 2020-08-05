# MemeSite-backend
ASP.NET Core 3.1 web.api
## Net.core WebApi
* Used technologies: 
  * C# Asp.net core 3.1,
  * Entity Framework Core,
  * Linq,
  * MSSQL server,
  * AutoMapper
  * FluentValidation
  * jwt bearer role based auth
  * some unit tests with Xunit</br>
## Basic Functionalities:
 * Registration/login,
 * Get comment list(assgined to meme or Paged list assigned to user),
 * Get meme paged list(by status, category or uploader's username),
 * insert/edit memes and comments(logged in users),
   * only owner can edit/delete
 * voting and add to favourites(logged in users),
 * Admin:
   * manage categories(crud),
   * manage memes(set status(archive, acceptance)/delete),
   * manage comments(delete/archive)</br>
### Frontend:
* https://github.com/kapiziom/MemeSite-Frontend

