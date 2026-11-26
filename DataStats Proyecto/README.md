# DataStats – Sistema de encuestas y análisis estadístico

Proyecto de escritorio desarrollado en **C# con Windows Forms para .NET 8**.

## Requisitos
- Windows 10 o Windows 11.
- Visual Studio 2022 con la carga de trabajo **Desarrollo de escritorio de .NET**.
- .NET 8 SDK.

## Abrir el proyecto
1. Extrae el ZIP.
2. Abre `DataStats.csproj` con Visual Studio 2022.
3. Espera a que Visual Studio cargue el proyecto.
4. Ejecuta con `F5`.

## Funcionalidades
- Panel principal con indicadores calculados sobre las encuestas almacenadas.
- Registro, edición, búsqueda y eliminación de encuestas.
- Variables: fecha, zona, proveedor, tipo de conexión, pago mensual, velocidad contratada, usuarios del hogar, satisfacción, estabilidad, frecuencia de problemas, recomendación y mejora solicitada.
- Estadísticas descriptivas: total de respuestas, media y mediana del gasto, proveedor más frecuente, estabilidad promedio y distribuciones con frecuencias y porcentajes.
- Exportación de los datos a CSV para continuar el análisis en Excel.
- Persistencia local en JSON.

## Interfaz adaptable
- Todas las medidas se escalan según el DPI de Windows (100 %, 125 %, 150 %...), por lo que ya no se cortan textos ni tarjetas.
- Los paneles usan acoplamiento y tablas, así que se ajustan al cambiar el tamaño de la ventana.
- La tabla de encuestas reparte el ancho entre las columnas sin necesidad de scroll horizontal.

## Almacenamiento
Los datos se guardan en:
`%LOCALAPPDATA%\DataStats\encuestas.json`

No se requiere servidor ni conexión a Internet para utilizar la aplicación.

## Importante para el informe académico
- La aplicación inicia sin datos de muestra. Los indicadores permanecen vacíos hasta registrar respuestas.
- Los resultados representan únicamente a las personas encuestadas.
- Indiquen en el documento la población, tamaño de muestra, fechas, zonas y método de selección.
- Si la selección se realiza por facilidad de acceso, descríbanla como muestra no probabilística por conveniencia.
- No ingresen nombres, correos, teléfonos ni direcciones exactas de los participantes.
- El sistema registra la velocidad contratada declarada por el usuario; no realiza pruebas técnicas de velocidad.

## Estructura
- `Program.cs`: punto de entrada.
- `MainForm.cs`: interfaz, navegación, tabla, indicadores, estadísticas y exportación.
- `SurveyForm.cs`: formulario para registrar y editar respuestas.
- `Models/SurveyResponse.cs`: modelo de datos.
- `Services/DataStore.cs`: persistencia local.
