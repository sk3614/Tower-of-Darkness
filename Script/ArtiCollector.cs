using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtiCollector : MonoBehaviour
{

    public MarketPlace market;
    public Item artidummy;


    public void Start()
    {

    }

    public void AddMarketList()
    {
        List<Artifact> artifacts = new List<Artifact>();

        if (ArtifactManager.S.RedOrb.able &&
            ArtifactManager.S.BlackOrb.able &&
            ArtifactManager.S.WhiteOrb.able &&
            ArtifactManager.S.RedOrb.able &&
            !ArtifactManager.S.BannerOfOrbCollector.getIs)
        {
            artifacts.Add(ArtifactManager.S.BannerOfOrbCollector);
        }
        if (ArtifactManager.S.MasterKey.able &&
            ArtifactManager.S.Pickax.able &&
            ArtifactManager.S.RingOfSpirit.able &&
            !ArtifactManager.S.DiggerKit.getIs)
        {
            artifacts.Add(ArtifactManager.S.DiggerKit);
        }
        if (ArtifactManager.S.RegeneRing.able &&
            ArtifactManager.S.DriedWater.able &&
            ArtifactManager.S.Thermometer.able &&
            ArtifactManager.S.Donguibogam.able &&
            !ArtifactManager.S.NecklaceOfVigor.getIs)
        {
            artifacts.Add(ArtifactManager.S.NecklaceOfVigor);
        }
        if (ArtifactManager.S.Compass.able &&
            ArtifactManager.S.PangeaGlobe.able &&
            !ArtifactManager.S.DivisamentDouMonde.getIs)
        {
            artifacts.Add(ArtifactManager.S.DivisamentDouMonde);
        }
        if (ArtifactManager.S.RawHam.able &&
            ArtifactManager.S.Seaweeds.able &&
            ArtifactManager.S.RawFish.able &&
            !ArtifactManager.S.MealKit.getIs)
        {
            artifacts.Add(ArtifactManager.S.MealKit);
        }
        if (ArtifactManager.S.AlchemyPot.able &&
            ArtifactManager.S.ScaleOfLife.able &&
            ArtifactManager.S.UrnOfLife.able &&
            ArtifactManager.S.TreasureOfWitch.able &&
            !ArtifactManager.S.WitchesBroom.getIs)
        {
            artifacts.Add(ArtifactManager.S.WitchesBroom);
        }



        for (int i = 0; i < artifacts.Count; i++)
        {
            GameObject go = Instantiate(market.P_slot, market.T_slots);
            TownShopSlot slot = go.GetComponent<TownShopSlot>();
            slot.artifact = artifacts[i];
            slot.item = artidummy;
            slot.itemPrice = 100;
            slot.itemNum = 1;
            slot.market = market;
            slot.SetShopSlot();
            market.slots.Add(go);
        }




      





    }

}
