using System;

public class ButtonIAP : ButtonBase
{
    public string productID;

    public string from = "shop";

    public override void OnClick()
    {
        // Purchaser.instance.BuyProductIDFrom(this.productID, this.from, null);
    }
}
