namespace Protocol.GameWebServerAndClient.ShareModels
{
    public enum ChangedAssetReason
    {
        ByUser,
        PurchasedGoods,
        Max
    }
    public enum GiveRewardReason
    {
        PurchasedGoods,
        PurchasedGoodsByAdmin,

        Max,
    }


    public enum AccountType
    {
        User,

        Admin,
        Max,
    }
}
