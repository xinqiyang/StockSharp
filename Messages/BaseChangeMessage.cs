namespace StockSharp.Messages
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Runtime.Serialization;
	using System.Xml.Serialization;

	using Ecng.Collections;

	using StockSharp.Localization;

	/// <summary>
	/// A message containing changes.
	/// </summary>
	/// <typeparam name="TMessage">Message type.</typeparam>
	/// <typeparam name="TField">Changes type.</typeparam>
	[DataContract]
	[Serializable]
	public abstract class BaseChangeMessage<TMessage, TField> :	BaseSubscriptionIdMessage<TMessage>,
		IServerTimeMessage, IGeneratedMessage
		where TMessage : BaseChangeMessage<TMessage, TField>, new()
	{
		/// <inheritdoc />
		[DataMember]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.ServerTimeKey,
			Description = LocalizedStrings.ChangeServerTimeKey,
			GroupName = LocalizedStrings.GeneralKey)]
		public DateTimeOffset ServerTime { get; set; }

		/// <inheritdoc />
		[DataMember]
		public DataType BuildFrom { get; set; }

		/// <summary>
		/// Changes.
		/// </summary>
		[Browsable(false)]
		//[DataMember]
		[XmlIgnore]
		public IDictionary<TField, object> Changes { get; } = new Dictionary<TField, object>();

		/// <summary>
		/// Initialize <see cref="BaseChangeMessage{TMessage,TField}"/>.
		/// </summary>
		/// <param name="type">Message type.</param>
		protected BaseChangeMessage(MessageTypes type)
			: base(type)
		{
		}

		/// <inheritdoc />
		public override void CopyTo(TMessage destination)
		{
			base.CopyTo(destination);

			destination.ServerTime = ServerTime;
			destination.BuildFrom = BuildFrom;

			destination.Changes.AddRange(Changes);
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return base.ToString() + $",T(S)={ServerTime:yyyy/MM/dd HH:mm:ss.fff}";
		}
	}
}