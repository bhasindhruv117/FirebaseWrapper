namespace FirebaseWrapper
{
    /// <summary>
    /// Common Firebase event and parameter names
    /// </summary>
    public static class FirebaseConstants
    {
        // Standard Firebase Analytics event names
        public static class Events
        {
            // App lifecycle events
            public const string APP_OPEN = "app_open";
            public const string APP_UPDATE = "app_update";
            public const string APP_EXCEPTION = "app_exception";
            
            // User engagement events
            public const string LOGIN = "login";
            public const string SIGN_UP = "sign_up";
            public const string LEVEL_START = "level_start";
            public const string LEVEL_END = "level_end";
            public const string LEVEL_UP = "level_up";
            public const string POST_SCORE = "post_score";
            public const string TUTORIAL_BEGIN = "tutorial_begin";
            public const string TUTORIAL_COMPLETE = "tutorial_complete";
            
            // In-app purchases
            public const string PURCHASE = "purchase";
            public const string ECOMMERCE_PURCHASE = "ecommerce_purchase";
            public const string PURCHASE_REFUND = "purchase_refund";
            public const string VIEW_ITEM = "view_item";
            public const string VIEW_ITEM_LIST = "view_item_list";
            public const string ADD_TO_CART = "add_to_cart";
            public const string ADD_TO_WISHLIST = "add_to_wishlist";
            public const string BEGIN_CHECKOUT = "begin_checkout";
            
            // Ad events
            public const string AD_IMPRESSION = "ad_impression";
            public const string AD_CLICK = "ad_click";
            public const string AD_VIEW = "ad_view";
            public const string AD_REWARD = "ad_reward";
            
            // Share events
            public const string SHARE = "share";
            public const string INVITE = "invite";
            
            // Custom game events
            public const string GAME_START = "game_start";
            public const string GAME_END = "game_end";
        }
        
        // Standard Firebase Analytics parameter names
        public static class Params
        {
            // Generic params
            public const string LEVEL = "level";
            public const string LEVEL_NAME = "level_name";
            public const string SCORE = "score";
            public const string SUCCESS = "success";
            public const string CHARACTER = "character";
            public const string METHOD = "method";
            
            // Item params
            public const string ITEM_ID = "item_id";
            public const string ITEM_NAME = "item_name";
            public const string ITEM_CATEGORY = "item_category";
            public const string PRICE = "price";
            public const string QUANTITY = "quantity";
            public const string CURRENCY = "currency";
            public const string VALUE = "value";
            
            // Ad params
            public const string AD_PLATFORM = "ad_platform";
            public const string AD_SOURCE = "ad_source";
            public const string AD_FORMAT = "ad_format";
            public const string AD_UNIT_NAME = "ad_unit_name";
            
            // App params
            public const string SOURCE = "source";
            public const string MEDIUM = "medium";
            public const string TERM = "term";
            public const string CONTENT = "content";
            public const string CAMPAIGN = "campaign";
            
            // Custom game params
            public const string DIFFICULTY = "difficulty";
            public const string GAME_MODE = "game_mode";
            public const string TIME_SPENT = "time_spent";
            public const string ATTEMPTS = "attempts";
        }
        
        // Standard Firebase user properties
        public static class UserProps
        {
            public const string ALLOW_AD_PERSONALIZATION = "allow_ad_personalization";
            public const string SIGN_UP_METHOD = "sign_up_method";
            public const string PLAYER_LEVEL = "player_level";
            public const string CHARACTER_TYPE = "character_type";
            public const string USER_TYPE = "user_type";
            public const string PURCHASED_STATUS = "purchased_status";
        }
    }
}
