

-- <Foreign Keys> --------------------------------------------------------------------

GO

GO

ALTER TABLE [dbo].[Subscription] DROP  [FK_Subscription_Brand_brand_id]

ALTER TABLE [dbo].[Subscription] DROP  [FK_Subscription_Product_product_id]

GO

ALTER TABLE [dbo].[PaymentDetail] DROP  [FK_PaymentDetail_Account_account_id]

GO

ALTER TABLE [dbo].[PaymentTransaction] DROP  [FK_PaymentTransaction_Order_order_id]

ALTER TABLE [dbo].[PaymentTransaction] DROP  [FK_PaymentTransaction_Payment_payment_id]

GO

ALTER TABLE [dbo].[Shipment] DROP  [FK_Shipment_Order_order_id]

GO

ALTER TABLE [dbo].[Payment] DROP  [FK_Payment_Order_order_id]

GO

ALTER TABLE [dbo].[Invoice] DROP  [FK_Invoice_Order_order_id]

ALTER TABLE [dbo].[Invoice] DROP  [FK_Invoice_Asset_asset_id]

GO

ALTER TABLE [dbo].[LineItem] DROP  [FK_LineItem_Order_order_id]

ALTER TABLE [dbo].[LineItem] DROP  [FK_LineItem_Listing_listing_id]

GO

ALTER TABLE [dbo].[Order] DROP  [FK_Order_Account_account_id]

ALTER TABLE [dbo].[Order] DROP  [FK_Order_Invoice_invoice_id]

ALTER TABLE [dbo].[Order] DROP  [FK_Order_Payment_payment_id]

ALTER TABLE [dbo].[Order] DROP  [FK_Order_Shipment_shipment_id]

GO

ALTER TABLE [dbo].[Listing] DROP  [FK_Listing_Brand_brand_id]

ALTER TABLE [dbo].[Listing] DROP  [FK_Listing_Product_product_id]

ALTER TABLE [dbo].[Listing] DROP  [FK_Listing_Promotion_promotion_id]

GO

GO

ALTER TABLE [dbo].[Product] DROP  [FK_Product_Brand_brand_id]

GO

GO

GO

-- </Foreign Keys> --------------------------------------------------------------------



-- <Unique Keys> --------------------------------------------------------------------

IF OBJECT_ID('dbo.UK_account_key', 'UQ') IS NOT NULL BEGIN -- multiple passes because of script limitations, thats fine. :)
	ALTER TABLE [dbo].[Account] 
		DROP CONSTRAINT UK_account_key
END
GO


-- </Unique Keys> --------------------------------------------------------------------


-- <Tables> --------------------------------------------------------------------

DROP TABLE [dbo].[Asset]
GO

DROP TABLE [dbo].[Account]
GO

DROP TABLE [dbo].[Subscription]
GO

DROP TABLE [dbo].[PaymentDetail]
GO

DROP TABLE [dbo].[PaymentTransaction]
GO

DROP TABLE [dbo].[Shipment]
GO

DROP TABLE [dbo].[Payment]
GO

DROP TABLE [dbo].[Invoice]
GO

DROP TABLE [dbo].[LineItem]
GO

DROP TABLE [dbo].[Order]
GO

DROP TABLE [dbo].[Listing]
GO

DROP TABLE [dbo].[Promotion]
GO

DROP TABLE [dbo].[Product]
GO

DROP TABLE [dbo].[Brand]
GO

DROP TABLE [dbo].[GlobalSetting]
GO

-- </Tables> --------------------------------------------------------------------

-- <Procedures> --------------------------------------------------------------------


DROP PROCEDURE [dbo].[spBrand_SyncUpdate]
GO

DROP PROCEDURE [dbo].[spBrand_SyncGetInvalid]
GO

DROP PROCEDURE [dbo].[spBrand_HydrateSyncUpdate]
GO

DROP PROCEDURE [dbo].[spBrand_HydrateSyncGetInvalid]
GO




DROP PROCEDURE [dbo].[spProduct_SyncUpdate]
GO

DROP PROCEDURE [dbo].[spProduct_SyncGetInvalid]
GO

DROP PROCEDURE [dbo].[spProduct_HydrateSyncUpdate]
GO

DROP PROCEDURE [dbo].[spProduct_HydrateSyncGetInvalid]
GO




DROP PROCEDURE [dbo].[spPromotion_SyncUpdate]
GO

DROP PROCEDURE [dbo].[spPromotion_SyncGetInvalid]
GO

DROP PROCEDURE [dbo].[spPromotion_HydrateSyncUpdate]
GO

DROP PROCEDURE [dbo].[spPromotion_HydrateSyncGetInvalid]
GO




DROP PROCEDURE [dbo].[spListing_SyncUpdate]
GO

DROP PROCEDURE [dbo].[spListing_SyncGetInvalid]
GO

DROP PROCEDURE [dbo].[spListing_HydrateSyncUpdate]
GO

DROP PROCEDURE [dbo].[spListing_HydrateSyncGetInvalid]
GO




DROP PROCEDURE [dbo].[spOrder_SyncUpdate]
GO

DROP PROCEDURE [dbo].[spOrder_SyncGetInvalid]
GO

DROP PROCEDURE [dbo].[spOrder_HydrateSyncUpdate]
GO

DROP PROCEDURE [dbo].[spOrder_HydrateSyncGetInvalid]
GO




DROP PROCEDURE [dbo].[spLineItem_SyncUpdate]
GO

DROP PROCEDURE [dbo].[spLineItem_SyncGetInvalid]
GO

DROP PROCEDURE [dbo].[spLineItem_HydrateSyncUpdate]
GO

DROP PROCEDURE [dbo].[spLineItem_HydrateSyncGetInvalid]
GO




DROP PROCEDURE [dbo].[spInvoice_SyncUpdate]
GO

DROP PROCEDURE [dbo].[spInvoice_SyncGetInvalid]
GO

DROP PROCEDURE [dbo].[spInvoice_HydrateSyncUpdate]
GO

DROP PROCEDURE [dbo].[spInvoice_HydrateSyncGetInvalid]
GO




DROP PROCEDURE [dbo].[spPayment_SyncUpdate]
GO

DROP PROCEDURE [dbo].[spPayment_SyncGetInvalid]
GO

DROP PROCEDURE [dbo].[spPayment_HydrateSyncUpdate]
GO

DROP PROCEDURE [dbo].[spPayment_HydrateSyncGetInvalid]
GO




DROP PROCEDURE [dbo].[spShipment_SyncUpdate]
GO

DROP PROCEDURE [dbo].[spShipment_SyncGetInvalid]
GO

DROP PROCEDURE [dbo].[spShipment_HydrateSyncUpdate]
GO

DROP PROCEDURE [dbo].[spShipment_HydrateSyncGetInvalid]
GO




DROP PROCEDURE [dbo].[spPaymentTransaction_SyncUpdate]
GO

DROP PROCEDURE [dbo].[spPaymentTransaction_SyncGetInvalid]
GO

DROP PROCEDURE [dbo].[spPaymentTransaction_HydrateSyncUpdate]
GO

DROP PROCEDURE [dbo].[spPaymentTransaction_HydrateSyncGetInvalid]
GO




DROP PROCEDURE [dbo].[spPaymentDetail_SyncUpdate]
GO

DROP PROCEDURE [dbo].[spPaymentDetail_SyncGetInvalid]
GO

DROP PROCEDURE [dbo].[spPaymentDetail_HydrateSyncUpdate]
GO

DROP PROCEDURE [dbo].[spPaymentDetail_HydrateSyncGetInvalid]
GO




DROP PROCEDURE [dbo].[spAccount_SyncUpdate]
GO

DROP PROCEDURE [dbo].[spAccount_SyncGetInvalid]
GO

DROP PROCEDURE [dbo].[spAccount_HydrateSyncUpdate]
GO

DROP PROCEDURE [dbo].[spAccount_HydrateSyncGetInvalid]
GO




DROP PROCEDURE [dbo].[spIndex_InvalidateAll]
GO
DROP PROCEDURE [dbo].[spIndex_InvalidateAggregates]
GO

DROP PROCEDURE [dbo].[spIndex_Status]
GO

DROP PROCEDURE [dbo].[spIndexHydrate_InvalidateAll]
GO

DROP PROCEDURE [dbo].[spIndexHydrate_InvalidateAggregates]
GO


DROP PROCEDURE [dbo].[spIndexHydrate_Status]
GO


-- <Procedures> --------------------------------------------------------------------
