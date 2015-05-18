using UnityEngine;
using System;
using System.Collections.Generic;
using Soomla.Store;
using System.Threading.Tasks;
using Parse;

public class SoomlaEventsHandling : MonoBehaviour
{
    public string key;
        /// <summary>
        /// Constructor.
        /// Subscribes to potential events.
        /// </summary>
        public SoomlaEventsHandling()
        {
            StoreEvents.OnMarketPurchase += onMarketPurchase;
            StoreEvents.OnMarketRefund += onMarketRefund;
            StoreEvents.OnItemPurchased += onItemPurchased;
            StoreEvents.OnGoodEquipped += onGoodEquipped;
            StoreEvents.OnGoodUnEquipped += onGoodUnequipped;
            StoreEvents.OnGoodUpgrade += onGoodUpgrade;
            StoreEvents.OnBillingSupported += onBillingSupported;
            StoreEvents.OnBillingNotSupported += onBillingNotSupported;
            StoreEvents.OnMarketPurchaseStarted += onMarketPurchaseStarted;
            StoreEvents.OnItemPurchaseStarted += onItemPurchaseStarted;
            StoreEvents.OnUnexpectedErrorInStore += onUnexpectedErrorInStore;
            StoreEvents.OnCurrencyBalanceChanged += onCurrencyBalanceChanged;
            StoreEvents.OnGoodBalanceChanged += onGoodBalanceChanged;
            StoreEvents.OnMarketPurchaseCancelled += onMarketPurchaseCancelled;
            StoreEvents.OnRestoreTransactionsStarted += onRestoreTransactionsStarted;
            StoreEvents.OnRestoreTransactionsFinished += onRestoreTransactionsFinished;
            StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialized;
#if UNITY_ANDROID && !UNITY_EDITOR
			StoreEvents.OnIabServiceStarted += onIabServiceStarted;
			StoreEvents.OnIabServiceStopped += onIabServiceStopped;
#endif
        }

        /// <summary>
        /// Handles a market purchase event.
        /// </summary>
        /// <param name="pvi">Purchasable virtual item.</param>
        /// <param name="purchaseToken">Purchase token.</param>
        public void onMarketPurchase(PurchasableVirtualItem pvi, string payload, Dictionary<string, string> extra)
        {
            
        }

        /// <summary>
        /// Handles a market refund event.
        /// </summary>
        /// <param name="pvi">Purchasable virtual item.</param>
        public void onMarketRefund(PurchasableVirtualItem pvi)
        {
            
        }

        /// <summary>
        /// Handles an item purchase event.
        /// </summary>
        /// <param name="pvi">Purchasable virtual item.</param>
        public void onItemPurchased(PurchasableVirtualItem pvi, string payload)
        {
            
        }

        /// <summary>
        /// Handles a good equipped event.
        /// </summary>
        /// <param name="good">Equippable virtual good.</param>
        public void onGoodEquipped(EquippableVG good)
        {

        }

        /// <summary>
        /// Handles a good unequipped event.
        /// </summary>
        /// <param name="good">Equippable virtual good.</param>
        public void onGoodUnequipped(EquippableVG good)
        {

        }

        /// <summary>
        /// Handles a good upgraded event.
        /// </summary>
        /// <param name="good">Virtual good that is being upgraded.</param>
        /// <param name="currentUpgrade">The current upgrade that the given virtual
        /// good is being upgraded to.</param>
        public void onGoodUpgrade(VirtualGood good, UpgradeVG currentUpgrade)
        {

        }

        /// <summary>
        /// Handles a billing supported event.
        /// </summary>
        public void onBillingSupported()
        {

        }

        /// <summary>
        /// Handles a billing NOT supported event.
        /// </summary>
        public void onBillingNotSupported()
        {

        }

        /// <summary>
        /// Handles a market purchase started event.
        /// </summary>
        /// <param name="pvi">Purchasable virtual item.</param>
        public void onMarketPurchaseStarted(PurchasableVirtualItem pvi)
        {

        }

        /// <summary>
        /// Handles an item purchase started event.
        /// </summary>
        /// <param name="pvi">Purchasable virtual item.</param>
        public void onItemPurchaseStarted(PurchasableVirtualItem pvi)
        {

        }

        /// <summary>
        /// Handles an item purchase cancelled event.
        /// </summary>
        /// <param name="pvi">Purchasable virtual item.</param>
        public void onMarketPurchaseCancelled(PurchasableVirtualItem pvi)
        {

        }

        /// <summary>
        /// Handles an unexpected error in store event.
        /// </summary>
        /// <param name="message">Error message.</param>
        public void onUnexpectedErrorInStore(string message)
        {

        }

        /// <summary>
        /// Handles a currency balance changed event.
        /// </summary>
        /// <param name="virtualCurrency">Virtual currency whose balance has changed.</param>
        /// <param name="balance">Balance of the given virtual currency.</param>
        /// <param name="amountAdded">Amount added to the balance.</param>
        public void onCurrencyBalanceChanged(VirtualCurrency virtualCurrency, int balance, int amountAdded)
        {
            if (Account.hasCurrentUser())
            {
                addCoin(amountAdded, key);
            } else {
                Debug.Log("User need to sign in");
                Notification.messageError("Bạn cần phải đăng nhập", "Lỗi đăng nhập", Notification.WARRNING_ERROR);
            }
        }

        /// <summary>
        /// Handles a good balance changed event.
        /// </summary>
        /// <param name="good">Virtual good whose balance has changed.</param>
        /// <param name="balance">Balance.</param>
        /// <param name="amountAdded">Amount added.</param>
        public void onGoodBalanceChanged(VirtualGood good, int balance, int amountAdded)
        {

        }

        /// <summary>
        /// Handles a restore Transactions process started event.
        /// </summary>
        public void onRestoreTransactionsStarted()
        {

        }

        /// <summary>
        /// Handles a restore transactions process finished event.
        /// </summary>
        /// <param name="success">If set to <c>true</c> success.</param>
        public void onRestoreTransactionsFinished(bool success)
        {

        }

        /// <summary>
        /// Handles a store controller initialized event.
        /// </summary>
        public void onSoomlaStoreInitialized()
        {

        }

#if UNITY_ANDROID && !UNITY_EDITOR
		public void onIabServiceStarted() {

		}
		public void onIabServiceStopped() {

		}
#endif

        private void addCoin(int amount, string key) {
            IDictionary<string, object> dict = new Dictionary<string, object>() { 
                 {"amount", amount},
                 {"key", key}
            };
            ParseCloud.CallFunctionAsync<IDictionary<string, object>>("addCoin", dict).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    using (IEnumerator<System.Exception> enumerator = t.Exception.InnerExceptions.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            ParseException error = (ParseException)enumerator.Current;
                            Debug.Log("Error: " + error.Code + ", " + error.Message);
                        }
                    }
                }
                else
                {
                    IDictionary<string, object> result = t.Result;
                    object errorCode;
                    if (result.TryGetValue("errorCode", out errorCode))
                    {
                        Debug.Log("Error: " + result["errorCode"] + ", " + result["message"]);
                    }
                    else
                    {
                        Debug.Log(result["coin"]);
                    }
                }
            });
        }

}
