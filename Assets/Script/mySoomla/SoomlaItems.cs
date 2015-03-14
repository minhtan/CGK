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
        return new VirtualCurrencyPack[] { COIN_PACK_10, COIN_PACK_60, COIN_PACK_120, COIN_PACK_250, COIN_PACK_600, COIN_PACK_1250};
    }

    public static VirtualCurrencyPack[] getMyCurrencyPack()
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
    public static VirtualCurrencyPack COIN_PACK_10 = new VirtualCurrencyPack(
        "10 Coins",                         // Name
        "Contains 10 coins",                // Description
        "coins_10",                         // Item ID
        10,                                 // Number of currencies in the pack
        "currency_coin",                    // The currency associated with this pack
        new PurchaseWithMarket(             // Purchase type
            "ten_coin_pack",     			// Product ID
            0.99)                           // Initial price
    );

    //pack 60 coin
    public static VirtualCurrencyPack COIN_PACK_60 = new VirtualCurrencyPack(
    "60 Coins",                         // Name
    "Contains 60 coins",                // Description
    "coins_60",                         // Item ID
    60,                                 // Number of currencies in the pack
    "currency_coin",                    // The currency associated with this pack
    new PurchaseWithMarket(             // Purchase type
        "ten_coin_pack",     			// Product ID
        4.99)                           // Initial price
    );

    //pack 120 coin
    public static VirtualCurrencyPack COIN_PACK_120 = new VirtualCurrencyPack(
    "120 Coins",                         // Name
    "Contains 120 coins",                // Description
    "coins_120",                         // Item ID
    120,                                 // Number of currencies in the pack
    "currency_coin",                    // The currency associated with this pack
    new PurchaseWithMarket(             // Purchase type
        "ten_coin_pack",     			// Product ID
        9.99)                           // Initial price
    );

    //pack 250 coin
    public static VirtualCurrencyPack COIN_PACK_250 = new VirtualCurrencyPack(
    "250 Coins",                         // Name
    "Contains 250 coins",                // Description
    "coins_250",                         // Item ID
    250,                                 // Number of currencies in the pack
    "currency_coin",                    // The currency associated with this pack
    new PurchaseWithMarket(             // Purchase type
        "ten_coin_pack",     			// Product ID
        19.99)                           // Initial price
    );

    //pack 600 coin
    public static VirtualCurrencyPack COIN_PACK_600 = new VirtualCurrencyPack(
    "600 Coins",                         // Name
    "Contains 600 coins",                // Description
    "coins_600",                         // Item ID
    600,                                 // Number of currencies in the pack
    "currency_coin",                    // The currency associated with this pack
    new PurchaseWithMarket(             // Purchase type
        "ten_coin_pack",     			// Product ID
        49.99)                           // Initial price
    );

    //pack 1250 coin
    public static VirtualCurrencyPack COIN_PACK_1250 = new VirtualCurrencyPack(
    "1250 Coins",                         // Name
    "Contains 1250 coins",                // Description
    "coins_1250",                         // Item ID
    1250,                                 // Number of currencies in the pack
    "currency_coin",                    // The currency associated with this pack
    new PurchaseWithMarket(             // Purchase type
        "ten_coin_pack",     			// Product ID
        99.99)                           // Initial price
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
