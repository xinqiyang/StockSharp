﻿namespace StockSharp.Algo.Indicators
{
	using System.ComponentModel.DataAnnotations;

	using Ecng.ComponentModel;

	using StockSharp.Messages;
	using StockSharp.Localization;

	/// <summary>
	/// Candle volume.
	/// </summary>
	/// <remarks>
	/// https://doc.stocksharp.com/topics/api/indicators/list_of_indicators/volume.html
	/// </remarks>
	[Display(
		ResourceType = typeof(LocalizedStrings),
		Name = LocalizedStrings.VolumeKey,
		Description = LocalizedStrings.CandleVolumeKey)]
	[IndicatorIn(typeof(CandleIndicatorValue))]
	[IndicatorOut(typeof(CandleIndicatorValue))]
	[Doc("topics/api/indicators/list_of_indicators/volume.html")]
	public class VolumeIndicator : BaseIndicator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="VolumeIndicator"/>.
		/// </summary>
		public VolumeIndicator()
		{
		}

		/// <inheritdoc />
		public override IndicatorMeasures Measure => IndicatorMeasures.Volume;

		/// <inheritdoc />
		protected override IIndicatorValue OnProcess(IIndicatorValue input)
		{
			if (input.IsFinal)
				IsFormed = true;

			return new DecimalIndicatorValue(this, input.GetValue<ICandleMessage>().TotalVolume)
			{
				IsFinal = input.IsFinal,
			};
		}
	}
}
