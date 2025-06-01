# LecturaUDB – Biblioteca Digital Interactiva

Este proyecto es una aplicación web ASP.NET MVC con conexión a SQL Server, desarrollada como parte de un proyecto académico. Permite a los usuarios explorar libros, calificarlos y ver un ranking en tiempo real.

---

## Funcionalidades principales

###  Página principal "Explorar Libros"
- Muestra libros en tarjetas visuales con imagen, título, autor, género y sinopsis truncada.
- Calificación con estrellas (1 a 5) sin necesidad de inicio de sesión.
- Filtros por título y género.
- Paginación (mínimo 5 libros por página).

### Página "Top Lecturas"
- Muestra las 5 obras mejor calificadas en tiempo real.
- Estilo visual con íconos de medallas.

### Gestión de base de datos
- CRUD completo para libros (crear, editar, eliminar).
- Las imágenes de portada se cargan por URL desde la base de datos.
- Calificaciones almacenadas con puntuación (1–5) y fecha/hora.

---

##  Manual del Programador

### Requisitos
- Visual Studio 2019 o superior
- .NET Framework 4.7.2
- SQL Server Express o superior
- Entity Framework 6

### Configuración
1. Restaurar la base de datos `BibliotecaDB` desde el archivo `.sql` en la carpeta `App_Data`.
2. En el archivo `Web.config`, edita la cadena de conexión:
   ```xml
   <connectionStrings>
     <add name="LecturaContext" connectionString="Data Source=TU_SERVIDOR;Initial Catalog=BibliotecaDB;Integrated Security=True" providerName="System.Data.SqlClient" />
   </connectionStrings>

## Propuestas de Mejora

1. Sistema de autenticación de usuarios
Permitir que los usuarios se registren y accedan al sistema. Esto permitiría vincular calificaciones, favoritos y reseñas a perfiles personales.

2. Reseñas y comentarios
Incorporar una sección donde los usuarios puedan escribir breves reseñas o comentarios sobre los libros.

3. Panel de administración
Implementar un backend protegido para administrar libros, calificaciones y usuarios de forma centralizada.

4. Diseño responsive
Adaptar el diseño visual para visualizarse correctamente en móviles y tablets.

5. Exportación del Top 5
Permitir descargar el ranking de libros mejor calificados en formatos PDF o CSV.

6. Filtros avanzados
Agregar filtros por autor, año de publicación y ordenar por calificación.


