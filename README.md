# School Notes App

Aplicación educativa desarrollada en **Unity 6** que simula el flujo completo de calificación y clasificación de estudiantes por parte de un docente.

El profesor carga un grupo de estudiantes, asigna manualmente si cada uno aprobó o reprobó (o arrastra sus tarjetas a las zonas de clasificación), y un validador verifica que cada decisión coincida con la nota final del estudiante.

## Funcionalidades

- **Carga de datos desde JSON** - los estudiantes se leen de `Assets/StreamingAssets/estudiantes.json`.
- **Panel de notas** - interfaz para registrar la calificación (Aprobado / Reprobado) de cada estudiante.
- **Validación de notas** - detecta y muestra con mensajes claros a los estudiantes mal aprobados, mal reprobados o sin calificar.
- **Navegación validada** - no se permite avanzar al siguiente panel hasta que la validación sea correcta.
- **Clasificación drag & drop** - el docente arrastra los botones de cada estudiante a las zonas "Aprobado" o "Reprobado"; al soltar se actualiza el estado del estudiante.
- **Reinicio del flujo** - devuelve los botones a su posición inicial, restaura los estados de los estudiantes y limpia la validación para volver a intentar.
- **Identidad visual generada** - iniciales del nombre con colores aleatorios y estilos de estado (éxito / error).

## Cómo ejecutar

1. Clona el repositorio.
2. Abre el proyecto con **Unity 6** (editor 6000.x).
3. Abre la escena `Assets/School Notes App - Main Folder/Scenes/Main App Scene.unity`.
4. Pulsa **Play**.

> El flujo de drag & drop requiere visualizar el juego en *Game view*.

## Datos de ejemplo

```json
{
  "estudiantes": [
    {
      "nombre": "Camila",
      "apellido": "Rodríguez",
      "codigo": "1001",
      "correo": "camila.rodriguez@colegio.edu.co",
      "notaFinal": 4.5
    }
  ]
}
```

La nota mínima de aprobación se configura en el componente `GameInstaller` de la escena (campo `passingGrade`, por defecto `3.0`).

## Tecnologías

- Unity 6 (6000.3.x)
- C# / .NET
- uGUI + TextMeshPro
- Patrones: Composition Root, Factory, MVP simplificado, Inyección de Dependencias manual

## Licencia

Uso educativo. Proyecto de prueba técnica.
