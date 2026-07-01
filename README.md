# Analizador de Inversiones - Plazos Fijos

**Institución:** Universidad Tecnológica Nacional (UTN - FRGP)  
**Carrera:** Tecnicatura en Programación  
**Evaluación:** Parcial 2  
**Autor:** Matías Martorano  

---

## 📌 Descripción del Proyecto

Aplicación de escritorio desarrollada para analizar y determinar la opción más rentable para invertir un capital fijo de **$850,000 ARS** en diferentes entidades bancarias (Banco Provincia, Banco Nación y Banco Hipotecario). 

El sistema evalúa el rendimiento del capital basándose en el promedio histórico de las tasas de interés de los últimos 3 años y proyecta las ganancias utilizando tres modalidades distintas de reinversión (interés compuesto).

## 🚀 Características Principales

* **Ingreso de Datos Históricos:** Matriz de entrada validada para asegurar que las tasas ingresadas sean valores numéricos estrictamente positivos, manejando configuraciones regionales (puntos y comas).
* **Cálculo de Promedios Automático:** Generación del promedio de la tasa anual por cada banco antes de habilitar la simulación.
* **Motor de Interés Compuesto:** * **Inversión Anual:** Retorno calculado con la tasa anual promedio.
    * **Inversión Trimestral:** Capitalización de intereses cada 3 meses.
    * **Inversión Mensual:** Capitalización de intereses cada 30 días.
* **Decisión Dinámica:** El sistema compara los 9 escenarios resultantes y emite una conclusión destacando la entidad y modalidad que maximizan el retorno.
* **Interfaz de Usuario Secuencial (UI):** Diseño clásico en Windows Forms, bloqueado a modificaciones externas (tamaño, reordenamiento de celdas) y con manejo de estados para guiar al usuario a través del flujo lógico (Ingreso -> Confirmación -> Cálculo).

## 🛠️ Tecnologías Utilizadas

* **Lenguaje:** C#
* **Framework:** .NET 8.0
* **Interfaz Gráfica:** Windows Forms (WinForms)
* **Arquitectura:** Programación orientada a eventos, generación dinámica de UI y validación estricta de estado.

## ⚙️ Uso y Ejecución

1. Clonar el repositorio.
2. Abrir la solución `.sln` en Visual Studio (2022 recomendado).
3. Compilar y ejecutar el proyecto (F5).
4. **Flujo de uso:**
   * Ingresar los porcentajes de los últimos 3 años en la primera tabla.
   * Presionar **1. Confirmar Datos** para calcular promedios y habilitar el siguiente paso.
   * Presionar **2. Calcular** para ejecutar las simulaciones y observar la recomendación final.