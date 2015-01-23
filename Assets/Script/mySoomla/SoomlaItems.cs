using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

public class SoomlaItems : IStoreAssets
{
    public int GetVersion()
    {
        return 0;
    }

    public VirtualCurrency[] GetCurrencies()
    {
        return new VirtualCurrency[] { COIN_CURRENCY };
    }

    public VirtualGood[] GetGoods()
    {
        return new VirtualGood[] {};
    }

    public VirtualCurrencyPack[] GetCurrencyPacks()
    {
        return new VirtualCurrencyPack[] { TEN_COIN_PACK, TEST_GOOD };
    }

    public VirtualCategory[] GetCategories()
    {
        return new VirtualCategory[] {};
    }

    /** Virtual Currencies **/
    public static VirtualCurrency COIN_CURRENCY = new VirtualCurrency(
        "Coin",                  			// Name
        "Used to place bets",            	// Description
        "currency_coin"                   	// Item ID
    );

    /** Virtual Currency Packs **/
    public static VirtualCurrencyPack TEN_COIN_PACK = new VirtualCurrencyPack(
        "10 Coins",                         // Name
        "Contains 10 coins",                // Description
        "coins_10",                         // Item ID
        10,                                 // Number of currencies in the pack
        "currency_coin",                    // The currency associated with this pack
        new PurchaseWithMarket(             // Purchase type
            "ten_coin_pack",     			// Product ID
            0.99)                           // Initial price
    );

    public static VirtualCurrencyPack TEST_GOOD = new VirtualCurrencyPack(
        "Test",                           	// Name
        "For test purpose only",	        // Description
        "test_me",           				// Item ID
        10,                                 // Number of currencies in the pack
        "currency_coin",                    // The currency associated with this pack
        new PurchaseWithMarket(        		// Purchase type
            "android.test.purchased",       // Product ID
            0)                            	// Initial price
    );

}
