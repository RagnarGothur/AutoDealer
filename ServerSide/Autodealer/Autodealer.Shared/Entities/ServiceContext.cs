//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
// Created via this command line: "C:\Users\Work\AppData\Roaming\MscrmTools\XrmToolBox\Plugins\DLaB.EarlyBoundGenerator\crmsvcutil.exe" /url:"https://autodealer.api.crm4.dynamics.com" /namespace:"Autodealer.Shared.Entities" /SuppressGeneratedCodeAttribute /out:"C:\Users\Work\Desktop\Projects\AutoDealer\ServerSide\Autodealer\Autodealer.Shared\Entities\ServiceContext.cs" /servicecontextname:"ServiceContext" /codecustomization:"DLaB.CrmSvcUtilExtensions.Entity.CustomizeCodeDomService,DLaB.CrmSvcUtilExtensions" /codegenerationservice:"DLaB.CrmSvcUtilExtensions.Entity.CustomCodeGenerationService,DLaB.CrmSvcUtilExtensions" /codewriterfilter:"DLaB.CrmSvcUtilExtensions.Entity.CodeWriterFilterService,DLaB.CrmSvcUtilExtensions" /namingservice:"DLaB.CrmSvcUtilExtensions.NamingService,DLaB.CrmSvcUtilExtensions" /metadataproviderservice:"DLaB.CrmSvcUtilExtensions.Entity.MetadataProviderService,DLaB.CrmSvcUtilExtensions" 
//------------------------------------------------------------------------------

[assembly: Microsoft.Xrm.Sdk.Client.ProxyTypesAssemblyAttribute()]
[assembly: System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "9.1.0.71")]

namespace Autodealer.Shared.Entities
{
	
	/// <summary>
	/// Represents a source of entities bound to a CRM service. It tracks and manages changes made to the retrieved entities.
	/// </summary>
	public partial class ServiceContext : Microsoft.Xrm.Sdk.Client.OrganizationServiceContext
	{
		
		/// <summary>
		/// Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public ServiceContext(Microsoft.Xrm.Sdk.IOrganizationService service) : 
				base(service)
		{
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="Autodealer.Shared.Entities.Account"/> entities.
		/// </summary>
		public System.Linq.IQueryable<Autodealer.Shared.Entities.Account> AccountSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<Autodealer.Shared.Entities.Account>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="Autodealer.Shared.Entities.autodeal_agreement"/> entities.
		/// </summary>
		public System.Linq.IQueryable<Autodealer.Shared.Entities.autodeal_agreement> autodeal_agreementSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<Autodealer.Shared.Entities.autodeal_agreement>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="Autodealer.Shared.Entities.autodeal_brand"/> entities.
		/// </summary>
		public System.Linq.IQueryable<Autodealer.Shared.Entities.autodeal_brand> autodeal_brandSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<Autodealer.Shared.Entities.autodeal_brand>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="Autodealer.Shared.Entities.autodeal_communication"/> entities.
		/// </summary>
		public System.Linq.IQueryable<Autodealer.Shared.Entities.autodeal_communication> autodeal_communicationSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<Autodealer.Shared.Entities.autodeal_communication>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="Autodealer.Shared.Entities.autodeal_credit"/> entities.
		/// </summary>
		public System.Linq.IQueryable<Autodealer.Shared.Entities.autodeal_credit> autodeal_creditSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<Autodealer.Shared.Entities.autodeal_credit>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="Autodealer.Shared.Entities.autodeal_invoice"/> entities.
		/// </summary>
		public System.Linq.IQueryable<Autodealer.Shared.Entities.autodeal_invoice> autodeal_invoiceSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<Autodealer.Shared.Entities.autodeal_invoice>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="Autodealer.Shared.Entities.autodeal_model"/> entities.
		/// </summary>
		public System.Linq.IQueryable<Autodealer.Shared.Entities.autodeal_model> autodeal_modelSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<Autodealer.Shared.Entities.autodeal_model>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="Autodealer.Shared.Entities.autodeal_vehicle"/> entities.
		/// </summary>
		public System.Linq.IQueryable<Autodealer.Shared.Entities.autodeal_vehicle> autodeal_vehicleSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<Autodealer.Shared.Entities.autodeal_vehicle>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="Autodealer.Shared.Entities.Contact"/> entities.
		/// </summary>
		public System.Linq.IQueryable<Autodealer.Shared.Entities.Contact> ContactSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<Autodealer.Shared.Entities.Contact>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="Autodealer.Shared.Entities.SystemUser"/> entities.
		/// </summary>
		public System.Linq.IQueryable<Autodealer.Shared.Entities.SystemUser> SystemUserSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<Autodealer.Shared.Entities.SystemUser>();
			}
		}
	}
	
	internal sealed class EntityOptionSetEnum
	{
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public static System.Nullable<int> GetEnum(Microsoft.Xrm.Sdk.Entity entity, string attributeLogicalName)
		{
			if (entity.Attributes.ContainsKey(attributeLogicalName))
			{
				Microsoft.Xrm.Sdk.OptionSetValue value = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(attributeLogicalName);
				if (value != null)
				{
					return value.Value;
				}
			}
			return null;
		}
	}
	
	/// <summary>
	/// Attribute to handle storing the OptionSet's Metadata.
	/// </summary>
	[System.AttributeUsageAttribute(System.AttributeTargets.Field)]
	public sealed class OptionSetMetadataAttribute : System.Attribute
	{
		
		/// <summary>
		/// Color of the OptionSetValue.
		/// </summary>
		public string Color { get; set; }
		
		/// <summary>
		/// Description of the OptionSetValue.
		/// </summary>
		public string Description { get; set; }
		
		/// <summary>
		/// Display order index of the OptionSetValue.
		/// </summary>
		public int DisplayIndex { get; set; }
		
		/// <summary>
		/// External value of the OptionSetValue.
		/// </summary>
		public string ExternalValue { get; set; }
		
		/// <summary>
		/// Name of the OptionSetValue.
		/// </summary>
		public string Name { get; set; }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="OptionSetMetadataAttribute"/> class.
		/// </summary>
		/// <param name="name">Name of the value.</param>
		/// <param name="displayIndex">Display order index of the value.</param>
		/// <param name="color">Color of the value.</param>
		/// <param name="description">Description of the value.</param>
		/// <param name="externalValue">External value of the value.</param>
		[System.Diagnostics.DebuggerNonUserCode()]
		public OptionSetMetadataAttribute(string name, int displayIndex, string color = null, string description = null, string externalValue = null)
		{
			this.Color = color;
			this.Description = description;
			this.ExternalValue = externalValue;
			this.DisplayIndex = displayIndex;
			this.Name = name;
		}
	}
	
	/// <summary>
	/// Extension class to handle retrieving of OptionSetMetadataAttribute.
	/// </summary>
	public static class OptionSetExtension
	{
		
		/// <summary>
		/// Returns the OptionSetMetadataAttribute for the given enum value
		/// </summary>
		/// <typeparam name="T">OptionSet Enum Type</typeparam>
		/// <param name="value">Enum Value with OptionSetMetadataAttribute</param>
		[System.Diagnostics.DebuggerNonUserCode()]
		public static OptionSetMetadataAttribute GetMetadata<T>(this T value)
			where T :  struct, System.IConvertible
		{
			System.Type enumType = typeof(T);
			if (!enumType.IsEnum)
			{
				throw new System.ArgumentException("T must be an enum!");
			}
			System.Reflection.MemberInfo[] members = enumType.GetMember(value.ToString());
			for (int i = 0; (i < members.Length); i++
			)
			{
				System.Attribute attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(members[i], typeof(OptionSetMetadataAttribute));
				if (attribute != null)
				{
					return ((OptionSetMetadataAttribute)(attribute));
				}
			}
			throw new System.ArgumentException("T must be an enum adorned with an OptionSetMetadataAttribute!");
		}
	}
}