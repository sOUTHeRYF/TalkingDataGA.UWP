using System;
using System.Collections.Generic;
using TalkingDataGAWP.command;
using TalkingDataGAWP.controllers;
using TalkingDataGAWP.Entity;

namespace TalkingDataGAWP
{
	public class TDGAVirtualCurrency
	{
		private static Dictionary<string, EventCharge> sCache = new Dictionary<string, EventCharge>();

		public static void onChargeRequest(string orderid, string iapid, double currencyAmount, string currencyType, double virtualCurrencyAmount, string paymentType)
		{
			if (!TalkingDataGA.isAlreadyInit)
			{
				Debugger.Log("SDK not initialized. TDGAVirtualCurrency.onChargeRequest()");
				return;
			}
			if (TDGAVirtualCurrency.sCache.ContainsKey(orderid))
			{
				return;
			}
			EventCharge eventCharge = new EventCharge(TDGASessionController.sGameSessionId, TDGAAccount.sAccount, TDGAMission.sMissionId, orderid, iapid, currencyAmount, currencyType, virtualCurrencyAmount, paymentType, EventCharge.ChargeStatus.REQUEST);
			TDGAEventListManager.addEvent(eventCharge, true);
			Type typeFromHandle = typeof(TDGAVirtualCurrency);
			lock (typeFromHandle)
			{
				TDGAVirtualCurrency.sCache.Add(orderid, eventCharge);
			}
		}

		public static void onChargeSuccess(string orderId)
		{
			if (!TalkingDataGA.isAlreadyInit)
			{
				Debugger.Log("SDK not initialized. TDGAVirtualCurrency.onChargeSuccess()");
				return;
			}
			EventCharge eventCharge = null;
			Type typeFromHandle = typeof(TDGAVirtualCurrency);
			lock (typeFromHandle)
			{
				TDGAVirtualCurrency.sCache.TryGetValue(orderId, ref eventCharge);
				TDGAVirtualCurrency.sCache.Remove(orderId);
			}
			if (eventCharge == null)
			{
				eventCharge = new EventCharge(TDGASessionController.sGameSessionId, TDGAAccount.sAccount, TDGAMission.sMissionId, orderId, string.Empty, 0.0, string.Empty, 0.0, string.Empty, EventCharge.ChargeStatus.SUCCESS);
			}
			TDGAEventListManager.addEvent(eventCharge, true);
		}

		public static void onReward(double currencyAmount, string reason)
		{
			if (!TalkingDataGA.isAlreadyInit)
			{
				Debugger.Log("SDK not initialized. TDGAVirtualCurrency.onReward()");
				return;
			}
			TDGAEventListManager.addEvent(new EventReward(TDGASessionController.sGameSessionId, TDGAAccount.sAccount, TDGAMission.sMissionId, currencyAmount, reason), true);
		}
	}
}
