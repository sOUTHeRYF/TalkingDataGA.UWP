using System;
using System.Collections.Generic;
using TalkingDataGAWP.command;
using TalkingDataGAWP.controllers;
using TalkingDataGAWP.Entity;

namespace TalkingDataGAWP
{
	public class TDGAAccount
	{
		public enum Gender
		{
			UNKNOW,
			MALE,
			FEMALE
		}

		public enum AccountType
		{
			ANONYMOUS,
			REGISTERED,
			SINA_WEIBO,
			QQ,
			QQ_WEIBO,
			ND91,
			TYPE1 = 11,
			TYPE2,
			TYPE3,
			TYPE4,
			TYPE5,
			TYPE6,
			TYPE7,
			TYPE8,
			TYPE9,
			TYPE10
		}

		private static string ACCOUNT_ID = "accountId";

		private static string ACCOUNT_LEVEL = "userLevel";

		private static string ACCOUNT_GENDER = "gender";

		private static string ACCOUNT_NAME = "accountName";

		private static string ACCOUNT_AGE = "age";

		private static string ACCOUNT_TYPE = "accountType";

		private static string PREF_ACCOUNT_FILE_NAME = "account_file";

		private static string LEVEL_UP_DURATION = "levelup_duration";

		private static string GAME_DURATION = "game_duration";

		private static string MIESSION_DURATION = "mission_duration";

		internal static TDGAAccount sAccount;

		private long mGameDurationStart;

		private long mGameDuration;

		private TDGAAccount.Gender _gender;

		private TDGAAccount.AccountType _accountType;

		private int _level;

		private string _gameServer;

		private string _accountName;

		private int _age;

		public TDGAAccount.Gender gender
		{
			get
			{
				return this._gender;
			}
		}

		public string accountID
		{
			get;
			internal set;
		}

		public TDGAAccount.AccountType accountType
		{
			get
			{
				return this._accountType;
			}
		}

		public int level
		{
			get
			{
				return this._level;
			}
		}

		public string gameServer
		{
			get
			{
				return this._gameServer;
			}
		}

		public string accountName
		{
			get
			{
				return this._accountName;
			}
		}

		public int age
		{
			get
			{
				return this._age;
			}
		}

		public void setGender(TDGAAccount.Gender gender)
		{
			this._gender = gender;
			this.updateAccountEvent();
		}

		public void setAccountType(TDGAAccount.AccountType type)
		{
			this._accountType = type;
			this.updateAccountEvent();
		}

		public void setLevel(int level)
		{
			if (level != this._level)
			{
				int level2 = this._level;
				this._level = level;
				this.updateAccountLevel(level2, level);
			}
		}

		public void setGameServer(string gameServer)
		{
			this._gameServer = gameServer;
			this.updateGameServer();
		}

		public void setAccountName(string accountName)
		{
			this._accountName = accountName;
			this.updateAccountEvent();
		}

		public void setAge(int age)
		{
			this._age = age;
			this.updateAccountEvent();
		}

		public static TDGAAccount setAccount(string accountid)
		{
			if (!TalkingDataGA.isAlreadyInit)
			{
				return null;
			}
			if (accountid == null || accountid.Trim().Equals(string.Empty))
			{
				return null;
			}
			TDGAAccount tDGAccount;
			if (TDGAAccount.sAccount.accountID.Equals(string.Empty))
			{
				TDGAAccount.sAccount.accountID = accountid;
				tDGAccount = TDGAAccount.sAccount;
			}
			else if (TDGAAccount.sAccount.accountID.Equals(accountid))
			{
				tDGAccount = TDGAAccount.sAccount;
			}
			else
			{
				string accountGameServer = TDGAPreference.getAccountGameServer(accountid);
				tDGAccount = TDGAAccount.getTDGAccount(accountid, accountGameServer);
				TDGAAccount.sAccount.updateGameDuration();
				TDGASessionController.changeAccount(TDGAAccount.sAccount, tDGAccount);
				TDGAAccount.sAccount = tDGAccount;
			}
			TDGAAccount.saveTDGAAccount(tDGAccount);
			TDGAPreference.setAccountID(accountid);
			return tDGAccount;
		}

		internal static TDGAAccount getTDGAccount()
		{
			string expr_05 = TDGAPreference.getAccountID();
			string accountGameServer = TDGAPreference.getAccountGameServer(expr_05);
			return TDGAAccount.getTDGAccount(expr_05, accountGameServer);
		}

		internal static TDGAAccount getTDGAccount(string accountId, string gameServer)
		{
			Dictionary<string, string> accountPreference = TDGAAccount.getAccountPreference(accountId, gameServer);
			TDGAAccount tDGAAccount = new TDGAAccount();
			tDGAAccount.accountID = accountId;
			tDGAAccount._gameServer = gameServer;
			if (accountPreference != null)
			{
				tDGAAccount._accountType = (TDGAAccount.AccountType)((!accountPreference.ContainsKey(TDGAAccount.ACCOUNT_TYPE)) ? 0 : int.Parse(accountPreference.get_Item(TDGAAccount.ACCOUNT_TYPE)));
				tDGAAccount._accountName = ((!accountPreference.ContainsKey(TDGAAccount.ACCOUNT_NAME)) ? string.Empty : accountPreference.get_Item(TDGAAccount.ACCOUNT_NAME));
				tDGAAccount._level = ((!accountPreference.ContainsKey(TDGAAccount.ACCOUNT_LEVEL)) ? 0 : int.Parse(accountPreference.get_Item(TDGAAccount.ACCOUNT_LEVEL)));
				tDGAAccount._age = ((!accountPreference.ContainsKey(TDGAAccount.ACCOUNT_AGE)) ? 0 : int.Parse(accountPreference.get_Item(TDGAAccount.ACCOUNT_AGE)));
				tDGAAccount._gender = (TDGAAccount.Gender)((!accountPreference.ContainsKey(TDGAAccount.ACCOUNT_GENDER)) ? 0 : int.Parse(accountPreference.get_Item(TDGAAccount.ACCOUNT_GENDER)));
				tDGAAccount.mGameDuration = ((!accountPreference.ContainsKey(TDGAAccount.GAME_DURATION)) ? 0L : long.Parse(accountPreference.get_Item(TDGAAccount.GAME_DURATION)));
				tDGAAccount.setDurationStartTime();
				TDGAAccount.saveTDGAAccount(tDGAAccount);
			}
			return tDGAAccount;
		}

		public void updateGameDuration()
		{
			Dictionary<string, string> accountPreference = TDGAAccount.getAccountPreference(this.accountID, this.gameServer);
			this.setKeyValue(TDGAAccount.GAME_DURATION, this.getCurrentDuration() + string.Empty, accountPreference);
			this.setDurationStartTime();
		}

		public void setGameDurationByMissionStart(string missionId)
		{
			long currentDuration = this.getCurrentDuration();
			Dictionary<string, string> accountPreference = TDGAAccount.getAccountPreference(this.accountID, this.gameServer);
			this.setKeyValue(TDGAAccount.MIESSION_DURATION + "_" + missionId, currentDuration + string.Empty, accountPreference);
		}

		public long getMissionDuration(string missionId)
		{
			Dictionary<string, string> accountPreference = TDGAAccount.getAccountPreference(this.accountID, this.gameServer);
			long num = (!accountPreference.ContainsKey(TDGAAccount.MIESSION_DURATION + "_" + missionId)) ? 0L : long.Parse(accountPreference.get_Item(TDGAAccount.MIESSION_DURATION + "_" + missionId));
			return this.getCurrentDuration() - num;
		}

		public long getLevelUpDuration()
		{
			Dictionary<string, string> accountPreference = TDGAAccount.getAccountPreference(this.accountID, this.gameServer);
			long num = (!accountPreference.ContainsKey(TDGAAccount.LEVEL_UP_DURATION)) ? 0L : long.Parse(accountPreference.get_Item(TDGAAccount.LEVEL_UP_DURATION));
			long currentDuration = this.getCurrentDuration();
			this.setKeyValue(TDGAAccount.LEVEL_UP_DURATION, currentDuration + string.Empty, accountPreference);
			return currentDuration - num;
		}

		internal long getCurrentDuration()
		{
			return this.mGameDuration + DateTimeUtils.getCurrentTime() - this.mGameDurationStart;
		}

		internal void setDurationStartTime()
		{
			this.mGameDurationStart = DateTimeUtils.getCurrentTime();
		}

		public static void saveTDGAAccount(TDGAAccount account)
		{
			Dictionary<string, string> accountPreference = TDGAAccount.getAccountPreference(account.accountID, account.gameServer);
			account.setKeyValueNotSave(TDGAAccount.ACCOUNT_ID, account.accountID, accountPreference);
			account.setKeyValueNotSave(TDGAAccount.ACCOUNT_TYPE, (int)account.accountType + string.Empty, accountPreference);
			account.setKeyValueNotSave(TDGAAccount.ACCOUNT_NAME, account.accountName, accountPreference);
			account.setKeyValueNotSave(TDGAAccount.ACCOUNT_LEVEL, account.level + string.Empty, accountPreference);
			account.setKeyValueNotSave(TDGAAccount.ACCOUNT_AGE, account.age + string.Empty, accountPreference);
			account.setKeyValueNotSave(TDGAAccount.ACCOUNT_GENDER, (int)account.gender + string.Empty, accountPreference);
			account.saveAccountPreference(accountPreference);
		}

		private void saveAccountPreference(Dictionary<string, string> dic)
		{
			IsolatedStorageUtils.Save<Dictionary<string, string>>(dic.get_Item(TDGAAccount.ACCOUNT_ID) + "_" + TDGAAccount.PREF_ACCOUNT_FILE_NAME, dic);
		}

		private static Dictionary<string, string> getAccountPreference(string accountid, string server)
		{
			Dictionary<string, string> dictionary = IsolatedStorageUtils.Get<Dictionary<string, string>>(accountid + "_" + TDGAAccount.PREF_ACCOUNT_FILE_NAME);
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, string>();
			}
			return dictionary;
		}

		private void updateAccountEvent()
		{
			TDGAAccount.saveTDGAAccount(this);
			TDGAEventListManager.addEvent(new EventUpdateAccount(TDGASessionController.sGameSessionId, this), true);
		}

		private void updateGameServer()
		{
			TDGAAccount arg_2E_0 = TDGAAccount.getTDGAccount(this.accountID, TDGAPreference.getAccountGameServer(this.accountID));
			TDGAPreference.setAccountGameServer(this.accountID, this.gameServer);
			TDGAAccount.saveTDGAAccount(this);
			TDGASessionController.restartSession(arg_2E_0, this);
			TDGAEventListManager.addEvent(new EventUpdateAccount(TDGASessionController.sGameSessionId, this));
		}

		private void updateAccountLevel(int last, int current)
		{
			TDGAAccount.saveTDGAAccount(this);
			long levelUpDuration = this.getLevelUpDuration();
			TDGAEventListManager.addEvent(new EventLevelUp(TDGASessionController.sGameSessionId, this, TDGAMission.sMissionId, last, levelUpDuration));
		}

		private void setKeyValue(string key, string value, Dictionary<string, string> dic)
		{
			this.setKeyValueNotSave(key, value, dic);
			this.saveAccountPreference(dic);
		}

		private void setKeyValueNotSave(string key, string value, Dictionary<string, string> dic)
		{
			if (dic.ContainsKey(key))
			{
				dic.set_Item(key, value);
				return;
			}
			dic.Add(key, value);
		}
	}
}
