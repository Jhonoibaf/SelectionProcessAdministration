# SelecctionPoccesAdministration

## Descripción general

El proyecto **SelectionProcessAdministration** es una aplicación web diseñada para ayudar a los reclutadores a gestionar una base de datos de candidatos para un proceso de selección. El proyecto sigue la arquitectura MVC e integra varias herramientas y prácticas modernas para un código limpio y escalable.

### Funcionalidades

- Consultar una lista de candidatos inscritos.
- Inscribir nuevos candidatos y sus experiencias profesionales.
- Editar la información de un candidato y sus experiencias profesionales.
- Eliminar candidatos del proceso de selección.
  
El proyecto utiliza el patrón **CQRS** (Segregación de Responsabilidad de Comandos y Consultas) mediante **MediatR** para manejar comandos y consultas, **FluentValidation** para la validación de datos, y **Entity Framework Core** (EF Core) como ORM para interactuar con una base de datos **SQL Server**.

---

## Estructura del proyecto

El proyecto sigue principios de **Arquitectura Limpia** para garantizar la separación de responsabilidades y una alta mantenibilidad.

### Carpetas

- **Recruiters.Application**: Contiene la lógica principal de la aplicación para manejar comandos, consultas, DTOs y validaciones.
  - **CandidatesAdministration**: Maneja comandos y consultas específicos de candidatos.
  - **ExperiencesAdministration**: Gestiona comandos relacionados con las experiencias de los candidatos.
  - **DTOs**: Objetos de Transferencia de Datos que desacoplan las entidades del dominio de los objetos de presentación.
  - **Mappers**: Lógica de mapeo entre DTOs y entidades del dominio usando AutoMapper.
  - **Validators**: Contiene las reglas de validación usando FluentValidation para los DTOs.

- **Recruiters.Domain**: Contiene las entidades del dominio (Candidate, CandidateExperience).

- **Recruiters.Infrastructure**: Contiene el contexto de la base de datos y las implementaciones de los repositorios para SQL Server.

- **SelectionProcessAdministration**: La capa web de la aplicación, que contiene los controladores MVC, vistas y modelos de vista.

---

## Herramientas y tecnologías

El proyecto está construido utilizando las siguientes bibliotecas y herramientas clave:

- **ASP.NET Core MVC**: Para estructurar la aplicación en controladores y vistas.
- **MediatR**: Para implementar el patrón CQRS, separando comandos y consultas.
- **Entity Framework Core**: ORM para gestionar el acceso a datos y las interacciones con la base de datos SQL Server.
- **AutoMapper**: Para mapear DTOs a modelos de dominio.
- **FluentValidation**: Para validar los datos en los DTOs antes de procesarlos.
- **Docker**: Para contenerizar la aplicación y garantizar la consistencia en diferentes entornos.

---

## Instrucciones para comenzar

### Requisitos previos

- **Docker** instalado en tu máquina.
- **SQL Server** o **SQL Server Express** ejecutándose localmente o en Docker.
- **.NET SDK 6.0** o superior instalado.

### Ejecución del proyecto

1. **Clonar el repositorio**: Clona el proyecto en tu entorno local usando:

   ```bash
   git clone <url-del-repositorio>
   cd SelectionProcessAdministration
2. **Construir y ejecutar los contenedores Docker**

    El proyecto utiliza Docker Compose para construir y ejecutar tanto la aplicación como la base de datos SQL Server como contenedores.
    Ejecuta el siguiente comando en el directorio raíz:

    ```bash
    docker-compose up --build

    Esto construirá las imágenes de Docker y ejecutará los contenedores para el proyecto SelectionProcessAdministration y la base de datos SQL Server.
3. **Configuración para desarrollo local**
    Si prefieres ejecutar el proyecto localmente sin Docker, asegúrate de configurar correctamente las variables de entorno en el archivo launchSettings.json. Necesitarás ajustar las cadenas de conexión en una variable de entorno **ConnectionStrings__AplicationDbConection.**
