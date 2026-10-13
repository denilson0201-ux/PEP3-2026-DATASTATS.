# DataStats

Sistema de encuesta digital de bajo costo para recolectar y analizar de forma automática la percepción de los usuarios sobre sus proveedores de Internet en Bolivia (Entel, Tigo, Viva y otros).

Proyecto de la materia **Probabilidad y Estadística** · Ingeniería de Sistemas · Cochabamba, Bolivia · 2026

## Objetivo

Desarrollar un sistema de encuesta digital de bajo costo, capaz de recolectar y analizar de forma automática la percepción de los usuarios sobre los proveedores de Internet, generando resultados estadísticos claros y visualizaciones comprensibles.

> Nota: el objetivo general y los específicos se están unificando con el documento del proyecto (ver Issue "Unificar objetivo general y específicos").

## Cómo funciona

| Componente | Función |
| --- | --- |
| Raspberry Pi | Equipo económico y autónomo donde corre el sistema, incluso sin Internet permanente |
| Python + pandas | Procesa el archivo CSV y calcula frecuencias, porcentajes, promedios y tablas de contingencia |
| Apache Superset | Tablero con gráficos interactivos (barras, circulares, tablas) |

## Equipo

| Integrante | Rol |
| --- | --- |
| Tiago Niño de Guzmán Salazar | Diseño y planificación de la encuesta |
| Liliana Chambi Colque | Recolección y organización de datos |
| Juan Denilson Mamani Quispe | Análisis de resultados |

- **Docente:** Diego Patrick Cárdenas Sejas
- **Carrera:** Ingeniería de Sistemas

## Estructura del repositorio

```
PEP3-2026-DataStats/
├── README.md
├── CONTRIBUTING.md          # reglas de trabajo del equipo (pendiente)
├── docs/
│   ├── 01-planificacion/    # Gantt, cronograma
│   ├── 02-requisitos/       # documento del proyecto, objetivos, metodología
│   ├── 03-diseno/           # arquitectura, diagramas, modelo de datos
│   ├── 04-presentaciones/   # presentaciones por entrega
│   ├── 05-reuniones/        # actas, una por reunión con fecha
│   └── 06-entregables/      # versiones finales por hito
├── src/                     # código fuente (script de Python, formulario)
├── tests/                   # pruebas
├── data/
│   ├── raw/                 # respuestas originales, no se modifican
│   └── processed/           # datos limpios
├── notebooks/               # análisis exploratorios
├── assets/                  # imágenes y recursos
└── .github/                 # plantillas de issues y pull requests
```

## Documentos clave

- Documento del proyecto: `docs/02-requisitos/DataStats_documento.docx`
- Diagrama de Gantt: `docs/01-planificacion/Diagrama_de_Gantt_DataStats.pdf`
- Tablero de tareas: pestaña **Projects** de este repositorio (pendiente de crear)

## Cronograma resumido (8 semanas)

| Hito | Semanas | Contenido |
| --- | --- | --- |
| 1. Planificación | 1-3 | Carátula, introducción, objetivos, metodología y cuestionario |
| 2. Sistema y recolección | 2-5 | Raspberry Pi, script de Python, Superset y aplicación de la encuesta |
| 3. Datos y análisis | 5-7 | Tabla general, resumen, gráficos, análisis y conclusiones |
| 4. Cierre y entrega | 8 | Bibliografía, revisión final y entrega |

## Estado actual

- [x] Documento del proyecto (primera versión)
- [x] Diagrama de Gantt
- [ ] Cuestionario final
- [ ] Sistema funcionando en la Raspberry Pi
- [ ] Encuesta aplicada

## Cómo ejecutar el proyecto

*Pendiente: se completará cuando exista el código (requisitos, instalación y uso).*

## Flujo de trabajo

1. Cada tarea es un **Issue** con responsable, hito y criterios de aceptación.
2. Los cambios se hacen en una rama (`docs/nombre-tarea`, `feature/nombre-tarea`) y entran a `main` mediante **pull request** revisado por otro integrante.
3. El avance se sigue en el tablero de **GitHub Projects**.
