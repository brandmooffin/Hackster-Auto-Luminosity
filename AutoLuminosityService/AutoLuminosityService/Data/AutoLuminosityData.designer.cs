﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoLuminosityService.Data
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="ProtHack")]
	public partial class AutoLuminosityDataDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertAutoLuminosity_Light(AutoLuminosity_Light instance);
    partial void UpdateAutoLuminosity_Light(AutoLuminosity_Light instance);
    partial void DeleteAutoLuminosity_Light(AutoLuminosity_Light instance);
    partial void InsertAutoLuminosity_RoomLightLink(AutoLuminosity_RoomLightLink instance);
    partial void UpdateAutoLuminosity_RoomLightLink(AutoLuminosity_RoomLightLink instance);
    partial void DeleteAutoLuminosity_RoomLightLink(AutoLuminosity_RoomLightLink instance);
    partial void InsertAutoLuminosity_Room(AutoLuminosity_Room instance);
    partial void UpdateAutoLuminosity_Room(AutoLuminosity_Room instance);
    partial void DeleteAutoLuminosity_Room(AutoLuminosity_Room instance);
    partial void InsertAutoLuminosity_User(AutoLuminosity_User instance);
    partial void UpdateAutoLuminosity_User(AutoLuminosity_User instance);
    partial void DeleteAutoLuminosity_User(AutoLuminosity_User instance);
    partial void InsertAutoLuminosity_Schedule(AutoLuminosity_Schedule instance);
    partial void UpdateAutoLuminosity_Schedule(AutoLuminosity_Schedule instance);
    partial void DeleteAutoLuminosity_Schedule(AutoLuminosity_Schedule instance);
    #endregion
		
		public AutoLuminosityDataDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["ProtHackConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public AutoLuminosityDataDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AutoLuminosityDataDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AutoLuminosityDataDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AutoLuminosityDataDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<AutoLuminosity_Light> AutoLuminosity_Lights
		{
			get
			{
				return this.GetTable<AutoLuminosity_Light>();
			}
		}
		
		public System.Data.Linq.Table<AutoLuminosity_RoomLightLink> AutoLuminosity_RoomLightLinks
		{
			get
			{
				return this.GetTable<AutoLuminosity_RoomLightLink>();
			}
		}
		
		public System.Data.Linq.Table<AutoLuminosity_Room> AutoLuminosity_Rooms
		{
			get
			{
				return this.GetTable<AutoLuminosity_Room>();
			}
		}
		
		public System.Data.Linq.Table<AutoLuminosity_User> AutoLuminosity_Users
		{
			get
			{
				return this.GetTable<AutoLuminosity_User>();
			}
		}
		
		public System.Data.Linq.Table<AutoLuminosity_Schedule> AutoLuminosity_Schedules
		{
			get
			{
				return this.GetTable<AutoLuminosity_Schedule>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.AutoLuminosity_Lights")]
	public partial class AutoLuminosity_Light : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _LightId;
		
		private int _UserId;
		
		private string _LightName;
		
		private System.DateTime _LightCreateDate;
		
		private bool _LightIsOn;
		
		private string _LightExternalId;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnLightIdChanging(int value);
    partial void OnLightIdChanged();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnLightNameChanging(string value);
    partial void OnLightNameChanged();
    partial void OnLightCreateDateChanging(System.DateTime value);
    partial void OnLightCreateDateChanged();
    partial void OnLightIsOnChanging(bool value);
    partial void OnLightIsOnChanged();
    partial void OnLightExternalIdChanging(string value);
    partial void OnLightExternalIdChanged();
    #endregion
		
		public AutoLuminosity_Light()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LightId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int LightId
		{
			get
			{
				return this._LightId;
			}
			set
			{
				if ((this._LightId != value))
				{
					this.OnLightIdChanging(value);
					this.SendPropertyChanging();
					this._LightId = value;
					this.SendPropertyChanged("LightId");
					this.OnLightIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="Int NOT NULL")]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LightName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string LightName
		{
			get
			{
				return this._LightName;
			}
			set
			{
				if ((this._LightName != value))
				{
					this.OnLightNameChanging(value);
					this.SendPropertyChanging();
					this._LightName = value;
					this.SendPropertyChanged("LightName");
					this.OnLightNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LightCreateDate", DbType="DateTime NOT NULL")]
		public System.DateTime LightCreateDate
		{
			get
			{
				return this._LightCreateDate;
			}
			set
			{
				if ((this._LightCreateDate != value))
				{
					this.OnLightCreateDateChanging(value);
					this.SendPropertyChanging();
					this._LightCreateDate = value;
					this.SendPropertyChanged("LightCreateDate");
					this.OnLightCreateDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LightIsOn", DbType="Bit NOT NULL")]
		public bool LightIsOn
		{
			get
			{
				return this._LightIsOn;
			}
			set
			{
				if ((this._LightIsOn != value))
				{
					this.OnLightIsOnChanging(value);
					this.SendPropertyChanging();
					this._LightIsOn = value;
					this.SendPropertyChanged("LightIsOn");
					this.OnLightIsOnChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LightExternalId", DbType="VarChar(50)")]
		public string LightExternalId
		{
			get
			{
				return this._LightExternalId;
			}
			set
			{
				if ((this._LightExternalId != value))
				{
					this.OnLightExternalIdChanging(value);
					this.SendPropertyChanging();
					this._LightExternalId = value;
					this.SendPropertyChanged("LightExternalId");
					this.OnLightExternalIdChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.AutoLuminosity_RoomLightLink")]
	public partial class AutoLuminosity_RoomLightLink : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _RoomLightLinkId;
		
		private int _RoomId;
		
		private int _LightId;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRoomLightLinkIdChanging(int value);
    partial void OnRoomLightLinkIdChanged();
    partial void OnRoomIdChanging(int value);
    partial void OnRoomIdChanged();
    partial void OnLightIdChanging(int value);
    partial void OnLightIdChanged();
    #endregion
		
		public AutoLuminosity_RoomLightLink()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RoomLightLinkId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int RoomLightLinkId
		{
			get
			{
				return this._RoomLightLinkId;
			}
			set
			{
				if ((this._RoomLightLinkId != value))
				{
					this.OnRoomLightLinkIdChanging(value);
					this.SendPropertyChanging();
					this._RoomLightLinkId = value;
					this.SendPropertyChanged("RoomLightLinkId");
					this.OnRoomLightLinkIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RoomId", DbType="Int NOT NULL")]
		public int RoomId
		{
			get
			{
				return this._RoomId;
			}
			set
			{
				if ((this._RoomId != value))
				{
					this.OnRoomIdChanging(value);
					this.SendPropertyChanging();
					this._RoomId = value;
					this.SendPropertyChanged("RoomId");
					this.OnRoomIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LightId", DbType="Int NOT NULL")]
		public int LightId
		{
			get
			{
				return this._LightId;
			}
			set
			{
				if ((this._LightId != value))
				{
					this.OnLightIdChanging(value);
					this.SendPropertyChanging();
					this._LightId = value;
					this.SendPropertyChanged("LightId");
					this.OnLightIdChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.AutoLuminosity_Rooms")]
	public partial class AutoLuminosity_Room : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _RoomId;
		
		private int _UserId;
		
		private string _RoomName;
		
		private System.DateTime _RoomCreateDate;
		
		private string _RoomExternalId;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRoomIdChanging(int value);
    partial void OnRoomIdChanged();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnRoomNameChanging(string value);
    partial void OnRoomNameChanged();
    partial void OnRoomCreateDateChanging(System.DateTime value);
    partial void OnRoomCreateDateChanged();
    partial void OnRoomExternalIdChanging(string value);
    partial void OnRoomExternalIdChanged();
    #endregion
		
		public AutoLuminosity_Room()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RoomId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int RoomId
		{
			get
			{
				return this._RoomId;
			}
			set
			{
				if ((this._RoomId != value))
				{
					this.OnRoomIdChanging(value);
					this.SendPropertyChanging();
					this._RoomId = value;
					this.SendPropertyChanged("RoomId");
					this.OnRoomIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="Int NOT NULL")]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RoomName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string RoomName
		{
			get
			{
				return this._RoomName;
			}
			set
			{
				if ((this._RoomName != value))
				{
					this.OnRoomNameChanging(value);
					this.SendPropertyChanging();
					this._RoomName = value;
					this.SendPropertyChanged("RoomName");
					this.OnRoomNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RoomCreateDate", DbType="DateTime NOT NULL")]
		public System.DateTime RoomCreateDate
		{
			get
			{
				return this._RoomCreateDate;
			}
			set
			{
				if ((this._RoomCreateDate != value))
				{
					this.OnRoomCreateDateChanging(value);
					this.SendPropertyChanging();
					this._RoomCreateDate = value;
					this.SendPropertyChanged("RoomCreateDate");
					this.OnRoomCreateDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RoomExternalId", DbType="VarChar(50)")]
		public string RoomExternalId
		{
			get
			{
				return this._RoomExternalId;
			}
			set
			{
				if ((this._RoomExternalId != value))
				{
					this.OnRoomExternalIdChanging(value);
					this.SendPropertyChanging();
					this._RoomExternalId = value;
					this.SendPropertyChanged("RoomExternalId");
					this.OnRoomExternalIdChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.AutoLuminosity_Users")]
	public partial class AutoLuminosity_User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserId;
		
		private string _UserEmail;
		
		private string _UserPassword;
		
		private System.DateTime _UserCreateDate;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnUserEmailChanging(string value);
    partial void OnUserEmailChanged();
    partial void OnUserPasswordChanging(string value);
    partial void OnUserPasswordChanged();
    partial void OnUserCreateDateChanging(System.DateTime value);
    partial void OnUserCreateDateChanged();
    #endregion
		
		public AutoLuminosity_User()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserEmail", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string UserEmail
		{
			get
			{
				return this._UserEmail;
			}
			set
			{
				if ((this._UserEmail != value))
				{
					this.OnUserEmailChanging(value);
					this.SendPropertyChanging();
					this._UserEmail = value;
					this.SendPropertyChanged("UserEmail");
					this.OnUserEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserPassword", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string UserPassword
		{
			get
			{
				return this._UserPassword;
			}
			set
			{
				if ((this._UserPassword != value))
				{
					this.OnUserPasswordChanging(value);
					this.SendPropertyChanging();
					this._UserPassword = value;
					this.SendPropertyChanged("UserPassword");
					this.OnUserPasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserCreateDate", DbType="DateTime NOT NULL")]
		public System.DateTime UserCreateDate
		{
			get
			{
				return this._UserCreateDate;
			}
			set
			{
				if ((this._UserCreateDate != value))
				{
					this.OnUserCreateDateChanging(value);
					this.SendPropertyChanging();
					this._UserCreateDate = value;
					this.SendPropertyChanged("UserCreateDate");
					this.OnUserCreateDateChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.AutoLuminosity_Schedules")]
	public partial class AutoLuminosity_Schedule : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ScheduleId;
		
		private string _ScheduleExecuteTime;
		
		private int _ScheduleAction;
		
		private int _ScheduleType;
		
		private int _ScheduleEntityId;
		
		private System.Nullable<int> _UserId;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnScheduleIdChanging(int value);
    partial void OnScheduleIdChanged();
    partial void OnScheduleExecuteTimeChanging(string value);
    partial void OnScheduleExecuteTimeChanged();
    partial void OnScheduleActionChanging(int value);
    partial void OnScheduleActionChanged();
    partial void OnScheduleTypeChanging(int value);
    partial void OnScheduleTypeChanged();
    partial void OnScheduleEntityIdChanging(int value);
    partial void OnScheduleEntityIdChanged();
    partial void OnUserIdChanging(System.Nullable<int> value);
    partial void OnUserIdChanged();
    #endregion
		
		public AutoLuminosity_Schedule()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScheduleId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ScheduleId
		{
			get
			{
				return this._ScheduleId;
			}
			set
			{
				if ((this._ScheduleId != value))
				{
					this.OnScheduleIdChanging(value);
					this.SendPropertyChanging();
					this._ScheduleId = value;
					this.SendPropertyChanged("ScheduleId");
					this.OnScheduleIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScheduleExecuteTime", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ScheduleExecuteTime
		{
			get
			{
				return this._ScheduleExecuteTime;
			}
			set
			{
				if ((this._ScheduleExecuteTime != value))
				{
					this.OnScheduleExecuteTimeChanging(value);
					this.SendPropertyChanging();
					this._ScheduleExecuteTime = value;
					this.SendPropertyChanged("ScheduleExecuteTime");
					this.OnScheduleExecuteTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScheduleAction", DbType="Int NOT NULL")]
		public int ScheduleAction
		{
			get
			{
				return this._ScheduleAction;
			}
			set
			{
				if ((this._ScheduleAction != value))
				{
					this.OnScheduleActionChanging(value);
					this.SendPropertyChanging();
					this._ScheduleAction = value;
					this.SendPropertyChanged("ScheduleAction");
					this.OnScheduleActionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScheduleType", DbType="Int NOT NULL")]
		public int ScheduleType
		{
			get
			{
				return this._ScheduleType;
			}
			set
			{
				if ((this._ScheduleType != value))
				{
					this.OnScheduleTypeChanging(value);
					this.SendPropertyChanging();
					this._ScheduleType = value;
					this.SendPropertyChanged("ScheduleType");
					this.OnScheduleTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScheduleEntityId", DbType="Int NOT NULL")]
		public int ScheduleEntityId
		{
			get
			{
				return this._ScheduleEntityId;
			}
			set
			{
				if ((this._ScheduleEntityId != value))
				{
					this.OnScheduleEntityIdChanging(value);
					this.SendPropertyChanging();
					this._ScheduleEntityId = value;
					this.SendPropertyChanged("ScheduleEntityId");
					this.OnScheduleEntityIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="Int")]
		public System.Nullable<int> UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
