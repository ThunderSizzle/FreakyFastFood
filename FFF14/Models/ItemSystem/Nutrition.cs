using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FFF.Models
{
	//todo Finish Comment Class
	/// <summary>
	/// Values for 2000 Calorie-Based Diet
	/// </summary>
	public enum NutritionBase
	{
		TotalFat = 65,
		SaturatedFat = 20,
		Cholesterol = 300,
		Sodium = 2400,
		TotalCarbohydrate = 300,
		DietaryFiber = 25
	}
	/// <summary>
	/// Handles all Nutrition Information for any Item, retaining all information regarding Nutrients.
	/// Also allows retrieval of percentages of nutrients based on a 2000 Calorie Diet.
	/// </summary>
	[ComplexType]
	public class Nutrition
	{
		/// <summary>
		/// Total Calories in Item
		/// </summary>
		public int Calories { get; set; }
		/// <summary>
		/// Calories From Fat
		/// </summary>
		public int CaloriesFromFat { get; set; }
		/// <summary>
		/// Protein, expressed in grams
		/// </summary>
		public int Protein { get; set; }
		/// <summary>
		/// Total Fat of Item, expressed in grams
		/// </summary>
		public int TotalFat { get; set; }
		/// <summary>
		/// Total Sodium of Item, expressed in milligrams
		/// </summary>
		public int Sodium { get; set; }
		/// <summary>
		/// Total Saturated Fat of item, expressed in grams
		/// </summary>
		public int SaturatedFat { get; set; }
		/// <summary>
		/// Total Trans Fat of Item, expressed in grams
		/// </summary>
		public int TransFat { get; set; }
		/// <summary>
		/// Total Cholesterol of Item, expressed in milligrams 
		/// </summary>
		public int Cholesterol { get; set; }
		/// <summary>
		/// Total Dietary Fiber of Item, expressed in grams
		/// </summary>
		public int DietaryFiber { get; set; }
		/// <summary>
		/// Total Sugar of Item, expressed in grams
		/// </summary>
		public int Sugars { get; set; }
		/// <summary>
		/// Total Carbs of item, expressed in grams
		/// </summary>
		public int TotalCarbohydrate { get; set;}
		/// <summary>
		/// Percent of Vitamin A, based on 2000 Calorie Diet
		/// </summary>
		public int VitaminA { get; set; }
		/// <summary>
		/// Percent of Vitamin C, based on 2000 Calorie Diet
		/// </summary>
		public int VitaminC { get; set; }
		/// <summary>
		/// Percent of Calcium, based on 2000 Calorie Diet
		/// </summary>
		public int Calcium { get; set; }
		/// <summary>
		/// Percent of Iron, based on 2000 Calorie Diet
		/// </summary>
		public int Iron { get; set; }
		/// <summary>
		/// Constructs a Default Nutrition Class with all Nutrients set to zero.
		/// </summary>
		public Nutrition()
		{
			this.Calories = 0;
			this.CaloriesFromFat = 0;
			this.Protein = 0;
			this.TotalFat = 0;
			this.Sodium = 0;
			this.SaturatedFat = 0;
			this.TransFat = 0;
			this.Cholesterol = 0;
			this.DietaryFiber = 0;
			this.Sugars = 0;
			this.TotalCarbohydrate = 0;
			this.VitaminA = 0;
			this.VitaminC = 0;
			this.Calcium = 0;
			this.Iron = 0;
		}

		/// <summary>
		/// Returns Total Fat Percentage, based on 2000 Calorie Diet
		/// </summary>
		/// <returns></returns>
		public int TotalFatPercent()
		{
			return (int) (this.TotalFat/ (double) NutritionBase.TotalFat) * 100;
		}
		/// <summary>
		/// Returns Total Saturated Fat Percentage, based on 2000 Calorie Diet
		/// </summary>
		/// <returns></returns>
		public int TotalSaturatedFatPercent()
		{
			return (int) (this.SaturatedFat/ (double) NutritionBase.SaturatedFat) * 100;
		}
		/// <summary>
		/// Returns Total Cholesterol Percentage, based on 2000 Calorie Diet
		/// </summary>
		/// <returns></returns>
		public int TotalCholesterolPercent()
		{
			return (int) (this.Cholesterol/ (double) NutritionBase.Cholesterol) * 100;
		}
		/// <summary>
		/// Returns Total Sodium Percentage, based on 2000 Calorie Diet
		/// </summary>
		/// <returns></returns>
		public int TotalSodiumPercent()
		{
			return (int) (this.Sodium/ (double) NutritionBase.Sodium) * 100;
		}
		/// <summary>
		/// Returns Total Carbohydrate Percentage, based on 2000 Calorie Diet
		/// </summary>
		/// <returns></returns>
		public int TotalCarbohydratePercent()
		{
			return (int) (this.TotalCarbohydrate/ (double) NutritionBase.TotalCarbohydrate) * 100;
		}
		/// <summary>
		/// Returns Total Dietary Fiber Percentage, based on 2000 Calorie Diet
		/// </summary>
		/// <returns></returns>
		public int TotalDietaryFiberPercent()
		{
			return (int) (this.DietaryFiber/ (double) NutritionBase.DietaryFiber) * 100;
		}
	}
}