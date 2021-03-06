//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Autodealer.Shared.Entities
{
	
	[System.Runtime.Serialization.DataContractAttribute()]
	public enum autodeal_agreementState
	{
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		Active = 0,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		Inactive = 1,
	}
	
	/// <summary>
	/// 
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("autodeal_agreement")]
	public partial class autodeal_agreement : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		public static class Fields
		{
			public const string autodeal_agreementId = "autodeal_agreementid";
			public const string Id = "autodeal_agreementid";
			public const string autodeal_autoId = "autodeal_autoid";
			public const string autodeal_contact = "autodeal_contact";
			public const string autodeal_creditamount = "autodeal_creditamount";
			public const string autodeal_creditamount_Base = "autodeal_creditamount_base";
			public const string autodeal_creditid = "autodeal_creditid";
			public const string autodeal_creditPeriod = "autodeal_creditperiod";
			public const string autodeal_date = "autodeal_date";
			public const string autodeal_fact = "autodeal_fact";
			public const string autodeal_factsum = "autodeal_factsum";
			public const string autodeal_factsum_Base = "autodeal_factsum_base";
			public const string autodeal_fullcreditamount = "autodeal_fullcreditamount";
			public const string autodeal_fullcreditamount_Base = "autodeal_fullcreditamount_base";
			public const string autodeal_initialfee = "autodeal_initialfee";
			public const string autodeal_initialfee_Base = "autodeal_initialfee_base";
			public const string autodeal_name = "autodeal_name";
			public const string autodeal_paymentplandate = "autodeal_paymentplandate";
			public const string autodeal_sum = "autodeal_sum";
			public const string autodeal_sum_Base = "autodeal_sum_base";
			public const string CreatedBy = "createdby";
			public const string CreatedOn = "createdon";
			public const string CreatedOnBehalfBy = "createdonbehalfby";
			public const string ExchangeRate = "exchangerate";
			public const string ImportSequenceNumber = "importsequencenumber";
			public const string ModifiedBy = "modifiedby";
			public const string ModifiedOn = "modifiedon";
			public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
			public const string OverriddenCreatedOn = "overriddencreatedon";
			public const string OwnerId = "ownerid";
			public const string OwningBusinessUnit = "owningbusinessunit";
			public const string OwningTeam = "owningteam";
			public const string OwningUser = "owninguser";
			public const string StateCode = "statecode";
			public const string StatusCode = "statuscode";
			public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
			public const string TransactionCurrencyId = "transactioncurrencyid";
			public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
			public const string VersionNumber = "versionnumber";
			public const string autodeal_autodeal_credit_autodeal_agreement_creditid = "autodeal_autodeal_credit_autodeal_agreement_creditid";
			public const string autodeal_autodeal_vehicle_autodeal_agreement_autoId = "autodeal_autodeal_vehicle_autodeal_agreement_autoId";
			public const string autodeal_contact_autodeal_agreement_contact = "autodeal_contact_autodeal_agreement_contact";
			public const string lk_autodeal_agreement_createdby = "lk_autodeal_agreement_createdby";
			public const string lk_autodeal_agreement_createdonbehalfby = "lk_autodeal_agreement_createdonbehalfby";
			public const string lk_autodeal_agreement_modifiedby = "lk_autodeal_agreement_modifiedby";
			public const string lk_autodeal_agreement_modifiedonbehalfby = "lk_autodeal_agreement_modifiedonbehalfby";
			public const string user_autodeal_agreement = "user_autodeal_agreement";
		}
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public autodeal_agreement() : 
				base(EntityLogicalName)
		{
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public autodeal_agreement(System.Guid id) : 
				base(EntityLogicalName, id)
		{
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public autodeal_agreement(string keyName, object keyValue) : 
				base(EntityLogicalName, keyName, keyValue)
		{
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public autodeal_agreement(Microsoft.Xrm.Sdk.KeyAttributeCollection keyAttributes) : 
				base(EntityLogicalName, keyAttributes)
		{
		}
		
		public const string EntityLogicalName = "autodeal_agreement";
		
		public const string EntitySchemaName = "autodeal_agreement";
		
		public const string PrimaryIdAttribute = "autodeal_agreementid";
		
		public const string PrimaryNameAttribute = "autodeal_name";
		
		public const string EntityLogicalCollectionName = "autodeal_agreements";
		
		public const string EntitySetName = "autodeal_agreements";
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		[System.Diagnostics.DebuggerNonUserCode()]
		private void OnPropertyChanged(string propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		private void OnPropertyChanging(string propertyName)
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, new System.ComponentModel.PropertyChangingEventArgs(propertyName));
			}
		}
		
		/// <summary>
		/// Уникальный идентификатор для экземпляров сущности.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_agreementid")]
		public System.Nullable<System.Guid> autodeal_agreementId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("autodeal_agreementid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_agreementId");
				this.SetAttributeValue("autodeal_agreementid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
				this.OnPropertyChanged("autodeal_agreementId");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_agreementid")]
		public override System.Guid Id
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return base.Id;
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.autodeal_agreementId = value;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_autoid")]
		public Microsoft.Xrm.Sdk.EntityReference autodeal_autoId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("autodeal_autoid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_autoId");
				this.SetAttributeValue("autodeal_autoid", value);
				this.OnPropertyChanged("autodeal_autoId");
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_contact")]
		public Microsoft.Xrm.Sdk.EntityReference autodeal_contact
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("autodeal_contact");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_contact");
				this.SetAttributeValue("autodeal_contact", value);
				this.OnPropertyChanged("autodeal_contact");
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_creditamount")]
		public Microsoft.Xrm.Sdk.Money autodeal_creditamount
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("autodeal_creditamount");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_creditamount");
				this.SetAttributeValue("autodeal_creditamount", value);
				this.OnPropertyChanged("autodeal_creditamount");
			}
		}
		
		/// <summary>
		/// Значение поля Сумма кредита в базовой валюте.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_creditamount_base")]
		public Microsoft.Xrm.Sdk.Money autodeal_creditamount_Base
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("autodeal_creditamount_base");
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_creditid")]
		public Microsoft.Xrm.Sdk.EntityReference autodeal_creditid
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("autodeal_creditid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_creditid");
				this.SetAttributeValue("autodeal_creditid", value);
				this.OnPropertyChanged("autodeal_creditid");
			}
		}
		
		/// <summary>
		/// Срок кредита в годах
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_creditperiod")]
		public System.Nullable<int> autodeal_creditPeriod
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("autodeal_creditperiod");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_creditPeriod");
				this.SetAttributeValue("autodeal_creditperiod", value);
				this.OnPropertyChanged("autodeal_creditPeriod");
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_date")]
		public System.Nullable<System.DateTime> autodeal_date
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("autodeal_date");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_date");
				this.SetAttributeValue("autodeal_date", value);
				this.OnPropertyChanged("autodeal_date");
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_fact")]
		public System.Nullable<bool> autodeal_fact
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<bool>>("autodeal_fact");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_fact");
				this.SetAttributeValue("autodeal_fact", value);
				this.OnPropertyChanged("autodeal_fact");
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_factsum")]
		public Microsoft.Xrm.Sdk.Money autodeal_factsum
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("autodeal_factsum");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_factsum");
				this.SetAttributeValue("autodeal_factsum", value);
				this.OnPropertyChanged("autodeal_factsum");
			}
		}
		
		/// <summary>
		/// Значение поля Оплаченная сумма в базовой валюте.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_factsum_base")]
		public Microsoft.Xrm.Sdk.Money autodeal_factsum_Base
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("autodeal_factsum_base");
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_fullcreditamount")]
		public Microsoft.Xrm.Sdk.Money autodeal_fullcreditamount
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("autodeal_fullcreditamount");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_fullcreditamount");
				this.SetAttributeValue("autodeal_fullcreditamount", value);
				this.OnPropertyChanged("autodeal_fullcreditamount");
			}
		}
		
		/// <summary>
		/// Значение поля Полная стоимость кредита в базовой валюте.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_fullcreditamount_base")]
		public Microsoft.Xrm.Sdk.Money autodeal_fullcreditamount_Base
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("autodeal_fullcreditamount_base");
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_initialfee")]
		public Microsoft.Xrm.Sdk.Money autodeal_initialfee
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("autodeal_initialfee");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_initialfee");
				this.SetAttributeValue("autodeal_initialfee", value);
				this.OnPropertyChanged("autodeal_initialfee");
			}
		}
		
		/// <summary>
		/// Значение поля Первоначальный взнос в базовой валюте.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_initialfee_base")]
		public Microsoft.Xrm.Sdk.Money autodeal_initialfee_Base
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("autodeal_initialfee_base");
			}
		}
		
		/// <summary>
		/// Имя настраиваемой сущности.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_name")]
		public string autodeal_name
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("autodeal_name");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_name");
				this.SetAttributeValue("autodeal_name", value);
				this.OnPropertyChanged("autodeal_name");
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_paymentplandate")]
		public System.Nullable<System.DateTime> autodeal_paymentplandate
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("autodeal_paymentplandate");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_paymentplandate");
				this.SetAttributeValue("autodeal_paymentplandate", value);
				this.OnPropertyChanged("autodeal_paymentplandate");
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_sum")]
		public Microsoft.Xrm.Sdk.Money autodeal_sum
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("autodeal_sum");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_sum");
				this.SetAttributeValue("autodeal_sum", value);
				this.OnPropertyChanged("autodeal_sum");
			}
		}
		
		/// <summary>
		/// Значение поля Сумма в базовой валюте.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_sum_base")]
		public Microsoft.Xrm.Sdk.Money autodeal_sum_Base
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("autodeal_sum_base");
			}
		}
		
		/// <summary>
		/// Уникальный идентификатор пользователя, создавшего запись.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
		public Microsoft.Xrm.Sdk.EntityReference CreatedBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdby");
			}
		}
		
		/// <summary>
		/// Дата и время создания записи.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdon")]
		public System.Nullable<System.DateTime> CreatedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("createdon");
			}
		}
		
		/// <summary>
		/// Уникальный идентификатор пользователя-представителя, создавшего запись.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
		public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdonbehalfby");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("CreatedOnBehalfBy");
				this.SetAttributeValue("createdonbehalfby", value);
				this.OnPropertyChanged("CreatedOnBehalfBy");
			}
		}
		
		/// <summary>
		/// Курс валюты, связанной с сущностью, по отношению к базовой валюте.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("exchangerate")]
		public System.Nullable<decimal> ExchangeRate
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<decimal>>("exchangerate");
			}
		}
		
		/// <summary>
		/// Порядковый номер импорта, в результате которого была создана эта запись.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("importsequencenumber")]
		public System.Nullable<int> ImportSequenceNumber
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("importsequencenumber");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("ImportSequenceNumber");
				this.SetAttributeValue("importsequencenumber", value);
				this.OnPropertyChanged("ImportSequenceNumber");
			}
		}
		
		/// <summary>
		/// Уникальный идентификатор пользователя, изменившего запись.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
		public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedby");
			}
		}
		
		/// <summary>
		/// Дата и время изменения записи.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedon")]
		public System.Nullable<System.DateTime> ModifiedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("modifiedon");
			}
		}
		
		/// <summary>
		/// Уникальный идентификатор пользователя-представителя, изменившего запись.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
		public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedonbehalfby");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("ModifiedOnBehalfBy");
				this.SetAttributeValue("modifiedonbehalfby", value);
				this.OnPropertyChanged("ModifiedOnBehalfBy");
			}
		}
		
		/// <summary>
		/// Дата и время переноса записи.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("overriddencreatedon")]
		public System.Nullable<System.DateTime> OverriddenCreatedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("overriddencreatedon");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("OverriddenCreatedOn");
				this.SetAttributeValue("overriddencreatedon", value);
				this.OnPropertyChanged("OverriddenCreatedOn");
			}
		}
		
		/// <summary>
		/// Идентификатор ответственного
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("ownerid")]
		public Microsoft.Xrm.Sdk.EntityReference OwnerId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("ownerid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("OwnerId");
				this.SetAttributeValue("ownerid", value);
				this.OnPropertyChanged("OwnerId");
			}
		}
		
		/// <summary>
		/// Уникальный идентификатор бизнес-единицы, владеющей этой записью
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owningbusinessunit")]
		public Microsoft.Xrm.Sdk.EntityReference OwningBusinessUnit
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("owningbusinessunit");
			}
		}
		
		/// <summary>
		/// Уникальный идентификатор рабочей группы, ответственной за запись.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owningteam")]
		public Microsoft.Xrm.Sdk.EntityReference OwningTeam
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("owningteam");
			}
		}
		
		/// <summary>
		/// Уникальный идентификатор пользователя, ответственного за запись.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owninguser")]
		public Microsoft.Xrm.Sdk.EntityReference OwningUser
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("owninguser");
			}
		}
		
		/// <summary>
		/// Статус Договор
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statecode")]
		public System.Nullable<Autodealer.Shared.Entities.autodeal_agreementState> StateCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statecode");
				if ((optionSet != null))
				{
					return ((Autodealer.Shared.Entities.autodeal_agreementState)(System.Enum.ToObject(typeof(Autodealer.Shared.Entities.autodeal_agreementState), optionSet.Value)));
				}
				else
				{
					return null;
				}
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("StateCode");
				if ((value == null))
				{
					this.SetAttributeValue("statecode", null);
				}
				else
				{
					this.SetAttributeValue("statecode", new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
				}
				this.OnPropertyChanged("StateCode");
			}
		}
		
		/// <summary>
		/// Причина состояния Договор
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statuscode")]
		public virtual autodeal_agreement_StatusCode? StatusCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((autodeal_agreement_StatusCode?)(EntityOptionSetEnum.GetEnum(this, "statuscode")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("StatusCode");
				this.SetAttributeValue("statuscode", value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null);
				this.OnPropertyChanged("StatusCode");
			}
		}
		
		/// <summary>
		/// Только для внутреннего использования.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("timezoneruleversionnumber")]
		public System.Nullable<int> TimeZoneRuleVersionNumber
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("timezoneruleversionnumber");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("TimeZoneRuleVersionNumber");
				this.SetAttributeValue("timezoneruleversionnumber", value);
				this.OnPropertyChanged("TimeZoneRuleVersionNumber");
			}
		}
		
		/// <summary>
		/// Уникальный идентификатор валюты, связанной с сущностью.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("transactioncurrencyid")]
		public Microsoft.Xrm.Sdk.EntityReference TransactionCurrencyId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("transactioncurrencyid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("TransactionCurrencyId");
				this.SetAttributeValue("transactioncurrencyid", value);
				this.OnPropertyChanged("TransactionCurrencyId");
			}
		}
		
		/// <summary>
		/// Код часового пояса, который использовался при создании записи.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("utcconversiontimezonecode")]
		public System.Nullable<int> UTCConversionTimeZoneCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("utcconversiontimezonecode");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("UTCConversionTimeZoneCode");
				this.SetAttributeValue("utcconversiontimezonecode", value);
				this.OnPropertyChanged("UTCConversionTimeZoneCode");
			}
		}
		
		/// <summary>
		/// Номер версии
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("versionnumber")]
		public System.Nullable<long> VersionNumber
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<long>>("versionnumber");
			}
		}
		
		/// <summary>
		/// 1:N autodeal_autodeal_agreement_autodeal_invoice_dogovorid
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("autodeal_autodeal_agreement_autodeal_invoice_dogovorid")]
		public System.Collections.Generic.IEnumerable<Autodealer.Shared.Entities.autodeal_invoice> autodeal_autodeal_agreement_autodeal_invoice_dogovorid
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Autodealer.Shared.Entities.autodeal_invoice>("autodeal_autodeal_agreement_autodeal_invoice_dogovorid", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_autodeal_agreement_autodeal_invoice_dogovorid");
				this.SetRelatedEntities<Autodealer.Shared.Entities.autodeal_invoice>("autodeal_autodeal_agreement_autodeal_invoice_dogovorid", null, value);
				this.OnPropertyChanged("autodeal_autodeal_agreement_autodeal_invoice_dogovorid");
			}
		}
		
		/// <summary>
		/// N:1 autodeal_autodeal_credit_autodeal_agreement_creditid
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_creditid")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("autodeal_autodeal_credit_autodeal_agreement_creditid")]
		public Autodealer.Shared.Entities.autodeal_credit autodeal_autodeal_credit_autodeal_agreement_creditid
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Autodealer.Shared.Entities.autodeal_credit>("autodeal_autodeal_credit_autodeal_agreement_creditid", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_autodeal_credit_autodeal_agreement_creditid");
				this.SetRelatedEntity<Autodealer.Shared.Entities.autodeal_credit>("autodeal_autodeal_credit_autodeal_agreement_creditid", null, value);
				this.OnPropertyChanged("autodeal_autodeal_credit_autodeal_agreement_creditid");
			}
		}
		
		/// <summary>
		/// N:1 autodeal_autodeal_vehicle_autodeal_agreement_autoId
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_autoid")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("autodeal_autodeal_vehicle_autodeal_agreement_autoId")]
		public Autodealer.Shared.Entities.autodeal_vehicle autodeal_autodeal_vehicle_autodeal_agreement_autoId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Autodealer.Shared.Entities.autodeal_vehicle>("autodeal_autodeal_vehicle_autodeal_agreement_autoId", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_autodeal_vehicle_autodeal_agreement_autoId");
				this.SetRelatedEntity<Autodealer.Shared.Entities.autodeal_vehicle>("autodeal_autodeal_vehicle_autodeal_agreement_autoId", null, value);
				this.OnPropertyChanged("autodeal_autodeal_vehicle_autodeal_agreement_autoId");
			}
		}
		
		/// <summary>
		/// N:1 autodeal_contact_autodeal_agreement_contact
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("autodeal_contact")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("autodeal_contact_autodeal_agreement_contact")]
		public Autodealer.Shared.Entities.Contact autodeal_contact_autodeal_agreement_contact
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Autodealer.Shared.Entities.Contact>("autodeal_contact_autodeal_agreement_contact", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("autodeal_contact_autodeal_agreement_contact");
				this.SetRelatedEntity<Autodealer.Shared.Entities.Contact>("autodeal_contact_autodeal_agreement_contact", null, value);
				this.OnPropertyChanged("autodeal_contact_autodeal_agreement_contact");
			}
		}
		
		/// <summary>
		/// N:1 lk_autodeal_agreement_createdby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_autodeal_agreement_createdby")]
		public Autodealer.Shared.Entities.SystemUser lk_autodeal_agreement_createdby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Autodealer.Shared.Entities.SystemUser>("lk_autodeal_agreement_createdby", null);
			}
		}
		
		/// <summary>
		/// N:1 lk_autodeal_agreement_createdonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_autodeal_agreement_createdonbehalfby")]
		public Autodealer.Shared.Entities.SystemUser lk_autodeal_agreement_createdonbehalfby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Autodealer.Shared.Entities.SystemUser>("lk_autodeal_agreement_createdonbehalfby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_autodeal_agreement_createdonbehalfby");
				this.SetRelatedEntity<Autodealer.Shared.Entities.SystemUser>("lk_autodeal_agreement_createdonbehalfby", null, value);
				this.OnPropertyChanged("lk_autodeal_agreement_createdonbehalfby");
			}
		}
		
		/// <summary>
		/// N:1 lk_autodeal_agreement_modifiedby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_autodeal_agreement_modifiedby")]
		public Autodealer.Shared.Entities.SystemUser lk_autodeal_agreement_modifiedby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Autodealer.Shared.Entities.SystemUser>("lk_autodeal_agreement_modifiedby", null);
			}
		}
		
		/// <summary>
		/// N:1 lk_autodeal_agreement_modifiedonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_autodeal_agreement_modifiedonbehalfby")]
		public Autodealer.Shared.Entities.SystemUser lk_autodeal_agreement_modifiedonbehalfby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Autodealer.Shared.Entities.SystemUser>("lk_autodeal_agreement_modifiedonbehalfby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_autodeal_agreement_modifiedonbehalfby");
				this.SetRelatedEntity<Autodealer.Shared.Entities.SystemUser>("lk_autodeal_agreement_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("lk_autodeal_agreement_modifiedonbehalfby");
			}
		}
		
		/// <summary>
		/// N:1 user_autodeal_agreement
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owninguser")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_autodeal_agreement")]
		public Autodealer.Shared.Entities.SystemUser user_autodeal_agreement
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Autodealer.Shared.Entities.SystemUser>("user_autodeal_agreement", null);
			}
		}
		
		/// <summary>
		/// Constructor for populating via LINQ queries given a LINQ anonymous type
		/// <param name="anonymousType">LINQ anonymous type.</param>
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public autodeal_agreement(object anonymousType) : 
				this()
		{
            foreach (var p in anonymousType.GetType().GetProperties())
            {
                var value = p.GetValue(anonymousType, null);
                var name = p.Name.ToLower();
            
                if (name.EndsWith("enum") && value.GetType().BaseType == typeof(System.Enum))
                {
                    value = new Microsoft.Xrm.Sdk.OptionSetValue((int) value);
                    name = name.Remove(name.Length - "enum".Length);
                }
            
                switch (name)
                {
                    case "id":
                        base.Id = (System.Guid)value;
                        Attributes["autodeal_agreementid"] = base.Id;
                        break;
                    case "autodeal_agreementid":
                        var id = (System.Nullable<System.Guid>) value;
                        if(id == null){ continue; }
                        base.Id = id.Value;
                        Attributes[name] = base.Id;
                        break;
                    case "formattedvalues":
                        // Add Support for FormattedValues
                        FormattedValues.AddRange((Microsoft.Xrm.Sdk.FormattedValueCollection)value);
                        break;
                    default:
                        Attributes[name] = value;
                        break;
                }
            }
		}
	}
}