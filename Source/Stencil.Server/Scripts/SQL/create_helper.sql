
-- <Tables> --------------------------------------------------------------------

CREATE TABLE [dbo].[GlobalSetting] (
	 [global_setting_id] uniqueidentifier NOT NULL
    ,[name] nvarchar(100) NOT NULL
    ,[value] nvarchar(max) NULL
    
  ,CONSTRAINT [PK_GlobalSetting] PRIMARY KEY CLUSTERED 
  (
	  [global_setting_id] ASC
  )
)

GO


CREATE TABLE [dbo].[Brand] (
	 [brand_id] uniqueidentifier NOT NULL
    ,[brand_name] nvarchar(100) NOT NULL
    ,[created_utc] DATETIMEOFFSET(0) NOT NULL
    ,[updated_utc] DATETIMEOFFSET(0) NOT NULL
    ,[deleted_utc] DATETIMEOFFSET(0) NULL
	,[sync_hydrate_utc] DATETIMEOFFSET(0) NULL
    ,[sync_success_utc] DATETIMEOFFSET(0) NULL
    ,[sync_invalid_utc] DATETIMEOFFSET(0) NULL
    ,[sync_attempt_utc] DATETIMEOFFSET(0) NULL
    ,[sync_agent] NVARCHAR(50) NULL
    ,[sync_log] NVARCHAR(MAX) NULL
  ,CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED 
  (
	  [brand_id] ASC
  )
)

GO


CREATE TABLE [dbo].[Product] (
	 [product_id] uniqueidentifier NOT NULL
    ,[brand_id] uniqueidentifier NOT NULL
    ,[product_name] nvarchar(100) NOT NULL
    ,[product_description] nvarchar(4000) NOT NULL
    ,[baseprice] decimal(5,2) NOT NULL
    ,[created_utc] DATETIMEOFFSET(0) NOT NULL
    ,[updated_utc] DATETIMEOFFSET(0) NOT NULL
    ,[deleted_utc] DATETIMEOFFSET(0) NULL
	,[sync_hydrate_utc] DATETIMEOFFSET(0) NULL
    ,[sync_success_utc] DATETIMEOFFSET(0) NULL
    ,[sync_invalid_utc] DATETIMEOFFSET(0) NULL
    ,[sync_attempt_utc] DATETIMEOFFSET(0) NULL
    ,[sync_agent] NVARCHAR(50) NULL
    ,[sync_log] NVARCHAR(MAX) NULL
  ,CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
  (
	  [product_id] ASC
  )
)

GO


CREATE TABLE [dbo].[Promotion] (
	 [promotion_id] uniqueidentifier NOT NULL
    ,[promotion_type] int NOT NULL
    ,[promotion_description] nvarchar(500) NOT NULL
    ,[percent] decimal(5,2) NOT NULL
    ,[created_utc] DATETIMEOFFSET(0) NOT NULL
    ,[updated_utc] DATETIMEOFFSET(0) NOT NULL
    ,[deleted_utc] DATETIMEOFFSET(0) NULL
	,[sync_hydrate_utc] DATETIMEOFFSET(0) NULL
    ,[sync_success_utc] DATETIMEOFFSET(0) NULL
    ,[sync_invalid_utc] DATETIMEOFFSET(0) NULL
    ,[sync_attempt_utc] DATETIMEOFFSET(0) NULL
    ,[sync_agent] NVARCHAR(50) NULL
    ,[sync_log] NVARCHAR(MAX) NULL
  ,CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
  (
	  [promotion_id] ASC
  )
)

GO


CREATE TABLE [dbo].[Listing] (
	 [listing_id] uniqueidentifier NOT NULL
    ,[brand_id] uniqueidentifier NOT NULL
    ,[product_id] uniqueidentifier NOT NULL
    ,[promotion_id] uniqueidentifier NULL
    ,[listing_description] nvarchar(500) NULL
    ,[active] bit NOT NULL
    ,[expire_utc] datetimeoffset(0) NOT NULL
    ,[created_utc] DATETIMEOFFSET(0) NOT NULL
    ,[updated_utc] DATETIMEOFFSET(0) NOT NULL
    ,[deleted_utc] DATETIMEOFFSET(0) NULL
	,[sync_hydrate_utc] DATETIMEOFFSET(0) NULL
    ,[sync_success_utc] DATETIMEOFFSET(0) NULL
    ,[sync_invalid_utc] DATETIMEOFFSET(0) NULL
    ,[sync_attempt_utc] DATETIMEOFFSET(0) NULL
    ,[sync_agent] NVARCHAR(50) NULL
    ,[sync_log] NVARCHAR(MAX) NULL
  ,CONSTRAINT [PK_Listing] PRIMARY KEY CLUSTERED 
  (
	  [listing_id] ASC
  )
)

GO


CREATE TABLE [dbo].[Order] (
	 [order_id] uniqueidentifier NOT NULL
    ,[account_id] uniqueidentifier NOT NULL
    ,[invoice_id] uniqueidentifier NULL
    ,[payment_id] uniqueidentifier NULL
    ,[shipment_id] uniqueidentifier NULL
    ,[order_paid] bit NOT NULL
    ,[order_shipped] bit NOT NULL
    ,[order_status] int NOT NULL
    ,[created_utc] DATETIMEOFFSET(0) NOT NULL
    ,[updated_utc] DATETIMEOFFSET(0) NOT NULL
    ,[deleted_utc] DATETIMEOFFSET(0) NULL
	,[sync_hydrate_utc] DATETIMEOFFSET(0) NULL
    ,[sync_success_utc] DATETIMEOFFSET(0) NULL
    ,[sync_invalid_utc] DATETIMEOFFSET(0) NULL
    ,[sync_attempt_utc] DATETIMEOFFSET(0) NULL
    ,[sync_agent] NVARCHAR(50) NULL
    ,[sync_log] NVARCHAR(MAX) NULL
  ,CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
  (
	  [order_id] ASC
  )
)

GO


CREATE TABLE [dbo].[LineItem] (
	 [lineitem_id] uniqueidentifier NOT NULL
    ,[order_id] uniqueidentifier NOT NULL
    ,[listing_id] uniqueidentifier NOT NULL
    ,[lineitem_quantity] int NOT NULL
    ,[created_utc] DATETIMEOFFSET(0) NOT NULL
    ,[updated_utc] DATETIMEOFFSET(0) NOT NULL
    ,[deleted_utc] DATETIMEOFFSET(0) NULL
	,[sync_hydrate_utc] DATETIMEOFFSET(0) NULL
    ,[sync_success_utc] DATETIMEOFFSET(0) NULL
    ,[sync_invalid_utc] DATETIMEOFFSET(0) NULL
    ,[sync_attempt_utc] DATETIMEOFFSET(0) NULL
    ,[sync_agent] NVARCHAR(50) NULL
    ,[sync_log] NVARCHAR(MAX) NULL
  ,CONSTRAINT [PK_LineItem] PRIMARY KEY CLUSTERED 
  (
	  [lineitem_id] ASC
  )
)

GO


CREATE TABLE [dbo].[Invoice] (
	 [invoice_id] uniqueidentifier NOT NULL
    ,[order_id] uniqueidentifier NOT NULL
    ,[asset_id] uniqueidentifier NOT NULL
    ,[created_utc] DATETIMEOFFSET(0) NOT NULL
    ,[updated_utc] DATETIMEOFFSET(0) NOT NULL
    ,[deleted_utc] DATETIMEOFFSET(0) NULL
	,[sync_hydrate_utc] DATETIMEOFFSET(0) NULL
    ,[sync_success_utc] DATETIMEOFFSET(0) NULL
    ,[sync_invalid_utc] DATETIMEOFFSET(0) NULL
    ,[sync_attempt_utc] DATETIMEOFFSET(0) NULL
    ,[sync_agent] NVARCHAR(50) NULL
    ,[sync_log] NVARCHAR(MAX) NULL
  ,CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
  (
	  [invoice_id] ASC
  )
)

GO


CREATE TABLE [dbo].[Payment] (
	 [payment_id] uniqueidentifier NOT NULL
    ,[order_id] uniqueidentifier NOT NULL
    ,[payment_processed_successful] bit NOT NULL
    ,[card_type] int NOT NULL
    ,[card_number] nvarchar(50) NOT NULL
    ,[expire_date] DateTime NOT NULL
    ,[cvv] int NOT NULL
    ,[save_paymentdetails] bit NOT NULL
    ,[created_utc] DATETIMEOFFSET(0) NOT NULL
    ,[updated_utc] DATETIMEOFFSET(0) NOT NULL
    ,[deleted_utc] DATETIMEOFFSET(0) NULL
	,[sync_hydrate_utc] DATETIMEOFFSET(0) NULL
    ,[sync_success_utc] DATETIMEOFFSET(0) NULL
    ,[sync_invalid_utc] DATETIMEOFFSET(0) NULL
    ,[sync_attempt_utc] DATETIMEOFFSET(0) NULL
    ,[sync_agent] NVARCHAR(50) NULL
    ,[sync_log] NVARCHAR(MAX) NULL
  ,CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
  (
	  [payment_id] ASC
  )
)

GO


CREATE TABLE [dbo].[Shipment] (
	 [shipment_id] uniqueidentifier NOT NULL
    ,[order_id] uniqueidentifier NOT NULL
    ,[shipment_carriertype] int NOT NULL
    ,[shipment_processed_successful] bit NOT NULL
    ,[shipment_street] nvarchar(150) NOT NULL
    ,[shipment_city] nvarchar(100) NOT NULL
    ,[shipment_state] nvarchar(100) NOT NULL
    ,[shipment_country] nvarchar(50) NOT NULL
    ,[shipment_zip] int NOT NULL
    ,[created_utc] DATETIMEOFFSET(0) NOT NULL
    ,[updated_utc] DATETIMEOFFSET(0) NOT NULL
    ,[deleted_utc] DATETIMEOFFSET(0) NULL
	,[sync_hydrate_utc] DATETIMEOFFSET(0) NULL
    ,[sync_success_utc] DATETIMEOFFSET(0) NULL
    ,[sync_invalid_utc] DATETIMEOFFSET(0) NULL
    ,[sync_attempt_utc] DATETIMEOFFSET(0) NULL
    ,[sync_agent] NVARCHAR(50) NULL
    ,[sync_log] NVARCHAR(MAX) NULL
  ,CONSTRAINT [PK_Shipment] PRIMARY KEY CLUSTERED 
  (
	  [shipment_id] ASC
  )
)

GO


CREATE TABLE [dbo].[PaymentTransaction] (
	 [paymenttransaction_id] uniqueidentifier NOT NULL
    ,[order_id] uniqueidentifier NOT NULL
    ,[payment_id] uniqueidentifier NOT NULL
    ,[transaction_outcome] int NOT NULL
    ,[created_utc] DATETIMEOFFSET(0) NOT NULL
    ,[updated_utc] DATETIMEOFFSET(0) NOT NULL
    ,[deleted_utc] DATETIMEOFFSET(0) NULL
	,[sync_hydrate_utc] DATETIMEOFFSET(0) NULL
    ,[sync_success_utc] DATETIMEOFFSET(0) NULL
    ,[sync_invalid_utc] DATETIMEOFFSET(0) NULL
    ,[sync_attempt_utc] DATETIMEOFFSET(0) NULL
    ,[sync_agent] NVARCHAR(50) NULL
    ,[sync_log] NVARCHAR(MAX) NULL
  ,CONSTRAINT [PK_PaymentTransaction] PRIMARY KEY CLUSTERED 
  (
	  [paymenttransaction_id] ASC
  )
)

GO


CREATE TABLE [dbo].[PaymentDetail] (
	 [paymentdetail_id] uniqueidentifier NOT NULL
    ,[account_id] uniqueidentifier NOT NULL
    ,[card_type] int NOT NULL
    ,[card_number] nvarchar(50) NOT NULL
    ,[expire_date] DateTime NOT NULL
    ,[cvv] int NOT NULL
    ,[created_utc] DATETIMEOFFSET(0) NOT NULL
    ,[updated_utc] DATETIMEOFFSET(0) NOT NULL
    ,[deleted_utc] DATETIMEOFFSET(0) NULL
	,[sync_hydrate_utc] DATETIMEOFFSET(0) NULL
    ,[sync_success_utc] DATETIMEOFFSET(0) NULL
    ,[sync_invalid_utc] DATETIMEOFFSET(0) NULL
    ,[sync_attempt_utc] DATETIMEOFFSET(0) NULL
    ,[sync_agent] NVARCHAR(50) NULL
    ,[sync_log] NVARCHAR(MAX) NULL
  ,CONSTRAINT [PK_PaymentDetail] PRIMARY KEY CLUSTERED 
  (
	  [paymentdetail_id] ASC
  )
)

GO


CREATE TABLE [dbo].[Subscription] (
	 [subscription_id] uniqueidentifier NOT NULL
    ,[brand_id] uniqueidentifier NOT NULL
    ,[event_name] nvarchar(50) NOT NULL
    ,[url] nvarchar(50) NOT NULL
    
  ,CONSTRAINT [PK_Subscription] PRIMARY KEY CLUSTERED 
  (
	  [subscription_id] ASC
  )
)

GO


CREATE TABLE [dbo].[Account] (
	 [account_id] uniqueidentifier NOT NULL
    ,[email] nvarchar(250) NOT NULL
    ,[password] nvarchar(250) NOT NULL
    ,[password_salt] nvarchar(50) NOT NULL
    ,[disabled] bit NOT NULL
    ,[api_key] nvarchar(50) NOT NULL
    ,[api_secret] nvarchar(50) NOT NULL
    ,[first_name] nvarchar(50) NULL
    ,[last_name] nvarchar(50) NULL
    ,[entitlements] nvarchar(250) NULL
    ,[password_reset_token] nvarchar(50) NULL
    ,[password_reset_utc] datetimeoffset(0) NULL
    ,[push_ios] nvarchar(100) NULL
    ,[push_google] nvarchar(100) NULL
    ,[push_microsoft] nvarchar(100) NULL
    ,[last_login_utc] datetimeoffset(0) NULL
    ,[last_login_platform] nvarchar(250) NULL
    ,[created_utc] DATETIMEOFFSET(0) NOT NULL
    ,[updated_utc] DATETIMEOFFSET(0) NOT NULL
    ,[deleted_utc] DATETIMEOFFSET(0) NULL
	,[sync_hydrate_utc] DATETIMEOFFSET(0) NULL
    ,[sync_success_utc] DATETIMEOFFSET(0) NULL
    ,[sync_invalid_utc] DATETIMEOFFSET(0) NULL
    ,[sync_attempt_utc] DATETIMEOFFSET(0) NULL
    ,[sync_agent] NVARCHAR(50) NULL
    ,[sync_log] NVARCHAR(MAX) NULL
  ,CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
  (
	  [account_id] ASC
  )
)

GO


CREATE TABLE [dbo].[Asset] (
	 [asset_id] uniqueidentifier NOT NULL
    ,[type] int NOT NULL
    ,[available] bit NOT NULL
    ,[resize_required] bit NOT NULL
    ,[encode_required] bit NOT NULL
    ,[resize_processing] bit NOT NULL
    ,[encode_processing] bit NOT NULL
    ,[thumb_small_dimensions] nvarchar(10) NULL
    ,[thumb_medium_dimensions] nvarchar(10) NULL
    ,[thumb_large_dimensions] nvarchar(10) NULL
    ,[resize_status] nvarchar(50) NULL
    ,[resize_attempts] int NOT NULL
    ,[resize_attempt_utc] datetimeoffset(0) NULL
    ,[encode_identifier] nvarchar(50) NULL
    ,[encode_status] nvarchar(50) NULL
    ,[raw_url] nvarchar(512) NULL
    ,[public_url] nvarchar(512) NULL
    ,[thumb_small_url] nvarchar(512) NULL
    ,[thumb_medium_url] nvarchar(512) NULL
    ,[thumb_large_url] nvarchar(512) NULL
    ,[encode_log] nvarchar(max) NULL
    ,[resize_log] nvarchar(max) NULL
    ,[dependencies] int NOT NULL
    ,[encode_attempts] int NOT NULL
    ,[encode_attempt_utc] datetimeoffset(0) NULL
    ,[resize_mode] nvarchar(20) NULL
    ,[created_utc] DATETIMEOFFSET(0) NOT NULL
    ,[updated_utc] DATETIMEOFFSET(0) NOT NULL
  ,CONSTRAINT [PK_Asset] PRIMARY KEY CLUSTERED 
  (
	  [asset_id] ASC
  )
)

GO


-- </Tables> --------------------------------------------------------------------


-- <Procedures> --------------------------------------------------------------------

CREATE PROCEDURE [dbo].[spIndex_InvalidateAll]
AS

   UPDATE [dbo].[Brand] SET [sync_success_utc] = NULL, [sync_log] = 'invalidateall'

   UPDATE [dbo].[Product] SET [sync_success_utc] = NULL, [sync_log] = 'invalidateall'

   UPDATE [dbo].[Promotion] SET [sync_success_utc] = NULL, [sync_log] = 'invalidateall'

   UPDATE [dbo].[Listing] SET [sync_success_utc] = NULL, [sync_log] = 'invalidateall'

   UPDATE [dbo].[Order] SET [sync_success_utc] = NULL, [sync_log] = 'invalidateall'

   UPDATE [dbo].[LineItem] SET [sync_success_utc] = NULL, [sync_log] = 'invalidateall'

   UPDATE [dbo].[Invoice] SET [sync_success_utc] = NULL, [sync_log] = 'invalidateall'

   UPDATE [dbo].[Payment] SET [sync_success_utc] = NULL, [sync_log] = 'invalidateall'

   UPDATE [dbo].[Shipment] SET [sync_success_utc] = NULL, [sync_log] = 'invalidateall'

   UPDATE [dbo].[PaymentTransaction] SET [sync_success_utc] = NULL, [sync_log] = 'invalidateall'

   UPDATE [dbo].[PaymentDetail] SET [sync_success_utc] = NULL, [sync_log] = 'invalidateall'

   UPDATE [dbo].[Account] SET [sync_success_utc] = NULL, [sync_log] = 'invalidateall'


GO

CREATE PROCEDURE [dbo].[spIndexHydrate_InvalidateAll]
AS

   UPDATE [dbo].[Brand] SET [sync_hydrate_utc] = NULL

   UPDATE [dbo].[Product] SET [sync_hydrate_utc] = NULL

   UPDATE [dbo].[Promotion] SET [sync_hydrate_utc] = NULL

   UPDATE [dbo].[Listing] SET [sync_hydrate_utc] = NULL

   UPDATE [dbo].[Order] SET [sync_hydrate_utc] = NULL

   UPDATE [dbo].[LineItem] SET [sync_hydrate_utc] = NULL

   UPDATE [dbo].[Invoice] SET [sync_hydrate_utc] = NULL

   UPDATE [dbo].[Payment] SET [sync_hydrate_utc] = NULL

   UPDATE [dbo].[Shipment] SET [sync_hydrate_utc] = NULL

   UPDATE [dbo].[PaymentTransaction] SET [sync_hydrate_utc] = NULL

   UPDATE [dbo].[PaymentDetail] SET [sync_hydrate_utc] = NULL

   UPDATE [dbo].[Account] SET [sync_hydrate_utc] = NULL


GO


CREATE PROCEDURE [dbo].[spIndex_InvalidateAggregates]
AS


GO


CREATE PROCEDURE [dbo].[spIndexHydrate_InvalidateAggregates]
AS


GO

CREATE PROCEDURE [dbo].[spIndex_Status]
AS

   SELECT 'Pending Items' as [Pending Items]

      ,(select count(1) from [dbo].[Account] where  [sync_success_utc] IS NULL) as [Account - 10]

      ,(select count(1) from [dbo].[Brand] where  [sync_success_utc] IS NULL) as [Brand - 15]

      ,(select count(1) from [dbo].[Product] where  [sync_success_utc] IS NULL) as [Product - 20]

      ,(select count(1) from [dbo].[Promotion] where  [sync_success_utc] IS NULL) as [Promotion - 25]

      ,(select count(1) from [dbo].[Listing] where  [sync_success_utc] IS NULL) as [Listing - 30]

      ,(select count(1) from [dbo].[Order] where  [sync_success_utc] IS NULL) as [Order - 35]

      ,(select count(1) from [dbo].[LineItem] where  [sync_success_utc] IS NULL) as [LineItem - 40]

      ,(select count(1) from [dbo].[Invoice] where  [sync_success_utc] IS NULL) as [Invoice - 45]

      ,(select count(1) from [dbo].[Payment] where  [sync_success_utc] IS NULL) as [Payment - 50]

      ,(select count(1) from [dbo].[Shipment] where  [sync_success_utc] IS NULL) as [Shipment - 55]

      ,(select count(1) from [dbo].[PaymentTransaction] where  [sync_success_utc] IS NULL) as [PaymentTransaction - 60]

      ,(select count(1) from [dbo].[PaymentDetail] where  [sync_success_utc] IS NULL) as [PaymentDetail - 65]

         

GO

CREATE PROCEDURE [dbo].[spIndexHydrate_Status]
AS

   SELECT 'Pending Items' as [Pending Items]

      ,(select count(1) from [dbo].[Account] where  [sync_hydrate_utc] IS NULL) as [Account - 10]

      ,(select count(1) from [dbo].[Brand] where  [sync_hydrate_utc] IS NULL) as [Brand - 15]

      ,(select count(1) from [dbo].[Product] where  [sync_hydrate_utc] IS NULL) as [Product - 20]

      ,(select count(1) from [dbo].[Promotion] where  [sync_hydrate_utc] IS NULL) as [Promotion - 25]

      ,(select count(1) from [dbo].[Listing] where  [sync_hydrate_utc] IS NULL) as [Listing - 30]

      ,(select count(1) from [dbo].[Order] where  [sync_hydrate_utc] IS NULL) as [Order - 35]

      ,(select count(1) from [dbo].[LineItem] where  [sync_hydrate_utc] IS NULL) as [LineItem - 40]

      ,(select count(1) from [dbo].[Invoice] where  [sync_hydrate_utc] IS NULL) as [Invoice - 45]

      ,(select count(1) from [dbo].[Payment] where  [sync_hydrate_utc] IS NULL) as [Payment - 50]

      ,(select count(1) from [dbo].[Shipment] where  [sync_hydrate_utc] IS NULL) as [Shipment - 55]

      ,(select count(1) from [dbo].[PaymentTransaction] where  [sync_hydrate_utc] IS NULL) as [PaymentTransaction - 60]

      ,(select count(1) from [dbo].[PaymentDetail] where  [sync_hydrate_utc] IS NULL) as [PaymentDetail - 65]

         

GO




CREATE PROCEDURE [dbo].[spBrand_SyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50)
AS
  SELECT [brand_id]
  FROM [dbo].[Brand]
  WHERE [sync_success_utc] IS NULL OR [deleted_utc] > [sync_success_utc]  OR [updated_utc] > [sync_success_utc]
  AND ISNULL([sync_agent],'') = ISNULL(@sync_agent,'')
  ORDER BY  -- oldest attempt, not attempted, failed -> then by change date  
	CASE WHEN NOT [sync_attempt_utc] IS NULL AND DATEDIFF(second,[sync_attempt_utc], GETUTCDATE()) > @allowableSecondsToProcessIndex  
			THEN 0 -- oldest in queue
		WHEN [sync_attempt_utc] IS NULL 
			THEN 1  -- synch is null , freshly invalidated 
		ELSE  2-- recently failed
	END asc
	,[sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spBrand_SyncUpdate]  
	 @brand_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_success_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)  
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNCH DATE
		UPDATE [dbo].[Brand]
		SET [sync_success_utc] = @sync_success_utc
			,[sync_attempt_utc] = NULL
			,[sync_invalid_utc] = NULL
			,[sync_log] = @sync_log
		WHERE [brand_id] = @brand_id
		AND [sync_success_utc] IS NULL
		AND (([sync_invalid_utc] IS NULL) OR ([sync_invalid_utc] <= @sync_success_utc))
	END
	ELSE
	BEGIN
		-- ON FAILED, SET SYNCH "ATTEMPT" DATE
		UPDATE [dbo].[Brand]
		SET [sync_attempt_utc] = GETUTCDATE()
			,[sync_log] = @sync_log
		WHERE [brand_id] = @brand_id
		AND [sync_success_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spBrand_HydrateSyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50) -- not used yet
AS
  SELECT [brand_id]
  FROM [dbo].[Brand]
  WHERE [sync_hydrate_utc] IS NULL
  ORDER BY [sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spBrand_HydrateSyncUpdate]  
	 @brand_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_hydrate_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)   -- not used yet
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNC DATE
		UPDATE [dbo].[Brand]
		SET [sync_hydrate_utc] = @sync_hydrate_utc
		WHERE [brand_id] = @brand_id
		AND [sync_hydrate_utc] IS NULL
	END
	ELSE
	BEGIN
		-- ON FAILED, ADD TO LOG
		UPDATE [dbo].[Brand]
		SET [sync_log] = @sync_log
		WHERE [brand_id] = @brand_id
		AND [sync_hydrate_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spProduct_SyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50)
AS
  SELECT [product_id]
  FROM [dbo].[Product]
  WHERE [sync_success_utc] IS NULL OR [deleted_utc] > [sync_success_utc]  OR [updated_utc] > [sync_success_utc]
  AND ISNULL([sync_agent],'') = ISNULL(@sync_agent,'')
  ORDER BY  -- oldest attempt, not attempted, failed -> then by change date  
	CASE WHEN NOT [sync_attempt_utc] IS NULL AND DATEDIFF(second,[sync_attempt_utc], GETUTCDATE()) > @allowableSecondsToProcessIndex  
			THEN 0 -- oldest in queue
		WHEN [sync_attempt_utc] IS NULL 
			THEN 1  -- synch is null , freshly invalidated 
		ELSE  2-- recently failed
	END asc
	,[sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spProduct_SyncUpdate]  
	 @product_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_success_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)  
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNCH DATE
		UPDATE [dbo].[Product]
		SET [sync_success_utc] = @sync_success_utc
			,[sync_attempt_utc] = NULL
			,[sync_invalid_utc] = NULL
			,[sync_log] = @sync_log
		WHERE [product_id] = @product_id
		AND [sync_success_utc] IS NULL
		AND (([sync_invalid_utc] IS NULL) OR ([sync_invalid_utc] <= @sync_success_utc))
	END
	ELSE
	BEGIN
		-- ON FAILED, SET SYNCH "ATTEMPT" DATE
		UPDATE [dbo].[Product]
		SET [sync_attempt_utc] = GETUTCDATE()
			,[sync_log] = @sync_log
		WHERE [product_id] = @product_id
		AND [sync_success_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spProduct_HydrateSyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50) -- not used yet
AS
  SELECT [product_id]
  FROM [dbo].[Product]
  WHERE [sync_hydrate_utc] IS NULL
  ORDER BY [sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spProduct_HydrateSyncUpdate]  
	 @product_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_hydrate_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)   -- not used yet
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNC DATE
		UPDATE [dbo].[Product]
		SET [sync_hydrate_utc] = @sync_hydrate_utc
		WHERE [product_id] = @product_id
		AND [sync_hydrate_utc] IS NULL
	END
	ELSE
	BEGIN
		-- ON FAILED, ADD TO LOG
		UPDATE [dbo].[Product]
		SET [sync_log] = @sync_log
		WHERE [product_id] = @product_id
		AND [sync_hydrate_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spPromotion_SyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50)
AS
  SELECT [promotion_id]
  FROM [dbo].[Promotion]
  WHERE [sync_success_utc] IS NULL OR [deleted_utc] > [sync_success_utc]  OR [updated_utc] > [sync_success_utc]
  AND ISNULL([sync_agent],'') = ISNULL(@sync_agent,'')
  ORDER BY  -- oldest attempt, not attempted, failed -> then by change date  
	CASE WHEN NOT [sync_attempt_utc] IS NULL AND DATEDIFF(second,[sync_attempt_utc], GETUTCDATE()) > @allowableSecondsToProcessIndex  
			THEN 0 -- oldest in queue
		WHEN [sync_attempt_utc] IS NULL 
			THEN 1  -- synch is null , freshly invalidated 
		ELSE  2-- recently failed
	END asc
	,[sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spPromotion_SyncUpdate]  
	 @promotion_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_success_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)  
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNCH DATE
		UPDATE [dbo].[Promotion]
		SET [sync_success_utc] = @sync_success_utc
			,[sync_attempt_utc] = NULL
			,[sync_invalid_utc] = NULL
			,[sync_log] = @sync_log
		WHERE [promotion_id] = @promotion_id
		AND [sync_success_utc] IS NULL
		AND (([sync_invalid_utc] IS NULL) OR ([sync_invalid_utc] <= @sync_success_utc))
	END
	ELSE
	BEGIN
		-- ON FAILED, SET SYNCH "ATTEMPT" DATE
		UPDATE [dbo].[Promotion]
		SET [sync_attempt_utc] = GETUTCDATE()
			,[sync_log] = @sync_log
		WHERE [promotion_id] = @promotion_id
		AND [sync_success_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spPromotion_HydrateSyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50) -- not used yet
AS
  SELECT [promotion_id]
  FROM [dbo].[Promotion]
  WHERE [sync_hydrate_utc] IS NULL
  ORDER BY [sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spPromotion_HydrateSyncUpdate]  
	 @promotion_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_hydrate_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)   -- not used yet
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNC DATE
		UPDATE [dbo].[Promotion]
		SET [sync_hydrate_utc] = @sync_hydrate_utc
		WHERE [promotion_id] = @promotion_id
		AND [sync_hydrate_utc] IS NULL
	END
	ELSE
	BEGIN
		-- ON FAILED, ADD TO LOG
		UPDATE [dbo].[Promotion]
		SET [sync_log] = @sync_log
		WHERE [promotion_id] = @promotion_id
		AND [sync_hydrate_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spListing_SyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50)
AS
  SELECT [listing_id]
  FROM [dbo].[Listing]
  WHERE [sync_success_utc] IS NULL OR [deleted_utc] > [sync_success_utc]  OR [updated_utc] > [sync_success_utc]
  AND ISNULL([sync_agent],'') = ISNULL(@sync_agent,'')
  ORDER BY  -- oldest attempt, not attempted, failed -> then by change date  
	CASE WHEN NOT [sync_attempt_utc] IS NULL AND DATEDIFF(second,[sync_attempt_utc], GETUTCDATE()) > @allowableSecondsToProcessIndex  
			THEN 0 -- oldest in queue
		WHEN [sync_attempt_utc] IS NULL 
			THEN 1  -- synch is null , freshly invalidated 
		ELSE  2-- recently failed
	END asc
	,[sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spListing_SyncUpdate]  
	 @listing_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_success_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)  
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNCH DATE
		UPDATE [dbo].[Listing]
		SET [sync_success_utc] = @sync_success_utc
			,[sync_attempt_utc] = NULL
			,[sync_invalid_utc] = NULL
			,[sync_log] = @sync_log
		WHERE [listing_id] = @listing_id
		AND [sync_success_utc] IS NULL
		AND (([sync_invalid_utc] IS NULL) OR ([sync_invalid_utc] <= @sync_success_utc))
	END
	ELSE
	BEGIN
		-- ON FAILED, SET SYNCH "ATTEMPT" DATE
		UPDATE [dbo].[Listing]
		SET [sync_attempt_utc] = GETUTCDATE()
			,[sync_log] = @sync_log
		WHERE [listing_id] = @listing_id
		AND [sync_success_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spListing_HydrateSyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50) -- not used yet
AS
  SELECT [listing_id]
  FROM [dbo].[Listing]
  WHERE [sync_hydrate_utc] IS NULL
  ORDER BY [sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spListing_HydrateSyncUpdate]  
	 @listing_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_hydrate_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)   -- not used yet
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNC DATE
		UPDATE [dbo].[Listing]
		SET [sync_hydrate_utc] = @sync_hydrate_utc
		WHERE [listing_id] = @listing_id
		AND [sync_hydrate_utc] IS NULL
	END
	ELSE
	BEGIN
		-- ON FAILED, ADD TO LOG
		UPDATE [dbo].[Listing]
		SET [sync_log] = @sync_log
		WHERE [listing_id] = @listing_id
		AND [sync_hydrate_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spOrder_SyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50)
AS
  SELECT [order_id]
  FROM [dbo].[Order]
  WHERE [sync_success_utc] IS NULL OR [deleted_utc] > [sync_success_utc]  OR [updated_utc] > [sync_success_utc]
  AND ISNULL([sync_agent],'') = ISNULL(@sync_agent,'')
  ORDER BY  -- oldest attempt, not attempted, failed -> then by change date  
	CASE WHEN NOT [sync_attempt_utc] IS NULL AND DATEDIFF(second,[sync_attempt_utc], GETUTCDATE()) > @allowableSecondsToProcessIndex  
			THEN 0 -- oldest in queue
		WHEN [sync_attempt_utc] IS NULL 
			THEN 1  -- synch is null , freshly invalidated 
		ELSE  2-- recently failed
	END asc
	,[sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spOrder_SyncUpdate]  
	 @order_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_success_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)  
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNCH DATE
		UPDATE [dbo].[Order]
		SET [sync_success_utc] = @sync_success_utc
			,[sync_attempt_utc] = NULL
			,[sync_invalid_utc] = NULL
			,[sync_log] = @sync_log
		WHERE [order_id] = @order_id
		AND [sync_success_utc] IS NULL
		AND (([sync_invalid_utc] IS NULL) OR ([sync_invalid_utc] <= @sync_success_utc))
	END
	ELSE
	BEGIN
		-- ON FAILED, SET SYNCH "ATTEMPT" DATE
		UPDATE [dbo].[Order]
		SET [sync_attempt_utc] = GETUTCDATE()
			,[sync_log] = @sync_log
		WHERE [order_id] = @order_id
		AND [sync_success_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spOrder_HydrateSyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50) -- not used yet
AS
  SELECT [order_id]
  FROM [dbo].[Order]
  WHERE [sync_hydrate_utc] IS NULL
  ORDER BY [sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spOrder_HydrateSyncUpdate]  
	 @order_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_hydrate_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)   -- not used yet
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNC DATE
		UPDATE [dbo].[Order]
		SET [sync_hydrate_utc] = @sync_hydrate_utc
		WHERE [order_id] = @order_id
		AND [sync_hydrate_utc] IS NULL
	END
	ELSE
	BEGIN
		-- ON FAILED, ADD TO LOG
		UPDATE [dbo].[Order]
		SET [sync_log] = @sync_log
		WHERE [order_id] = @order_id
		AND [sync_hydrate_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spLineItem_SyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50)
AS
  SELECT [lineitem_id]
  FROM [dbo].[LineItem]
  WHERE [sync_success_utc] IS NULL OR [deleted_utc] > [sync_success_utc]  OR [updated_utc] > [sync_success_utc]
  AND ISNULL([sync_agent],'') = ISNULL(@sync_agent,'')
  ORDER BY  -- oldest attempt, not attempted, failed -> then by change date  
	CASE WHEN NOT [sync_attempt_utc] IS NULL AND DATEDIFF(second,[sync_attempt_utc], GETUTCDATE()) > @allowableSecondsToProcessIndex  
			THEN 0 -- oldest in queue
		WHEN [sync_attempt_utc] IS NULL 
			THEN 1  -- synch is null , freshly invalidated 
		ELSE  2-- recently failed
	END asc
	,[sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spLineItem_SyncUpdate]  
	 @lineitem_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_success_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)  
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNCH DATE
		UPDATE [dbo].[LineItem]
		SET [sync_success_utc] = @sync_success_utc
			,[sync_attempt_utc] = NULL
			,[sync_invalid_utc] = NULL
			,[sync_log] = @sync_log
		WHERE [lineitem_id] = @lineitem_id
		AND [sync_success_utc] IS NULL
		AND (([sync_invalid_utc] IS NULL) OR ([sync_invalid_utc] <= @sync_success_utc))
	END
	ELSE
	BEGIN
		-- ON FAILED, SET SYNCH "ATTEMPT" DATE
		UPDATE [dbo].[LineItem]
		SET [sync_attempt_utc] = GETUTCDATE()
			,[sync_log] = @sync_log
		WHERE [lineitem_id] = @lineitem_id
		AND [sync_success_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spLineItem_HydrateSyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50) -- not used yet
AS
  SELECT [lineitem_id]
  FROM [dbo].[LineItem]
  WHERE [sync_hydrate_utc] IS NULL
  ORDER BY [sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spLineItem_HydrateSyncUpdate]  
	 @lineitem_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_hydrate_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)   -- not used yet
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNC DATE
		UPDATE [dbo].[LineItem]
		SET [sync_hydrate_utc] = @sync_hydrate_utc
		WHERE [lineitem_id] = @lineitem_id
		AND [sync_hydrate_utc] IS NULL
	END
	ELSE
	BEGIN
		-- ON FAILED, ADD TO LOG
		UPDATE [dbo].[LineItem]
		SET [sync_log] = @sync_log
		WHERE [lineitem_id] = @lineitem_id
		AND [sync_hydrate_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spInvoice_SyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50)
AS
  SELECT [invoice_id]
  FROM [dbo].[Invoice]
  WHERE [sync_success_utc] IS NULL OR [deleted_utc] > [sync_success_utc]  OR [updated_utc] > [sync_success_utc]
  AND ISNULL([sync_agent],'') = ISNULL(@sync_agent,'')
  ORDER BY  -- oldest attempt, not attempted, failed -> then by change date  
	CASE WHEN NOT [sync_attempt_utc] IS NULL AND DATEDIFF(second,[sync_attempt_utc], GETUTCDATE()) > @allowableSecondsToProcessIndex  
			THEN 0 -- oldest in queue
		WHEN [sync_attempt_utc] IS NULL 
			THEN 1  -- synch is null , freshly invalidated 
		ELSE  2-- recently failed
	END asc
	,[sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spInvoice_SyncUpdate]  
	 @invoice_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_success_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)  
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNCH DATE
		UPDATE [dbo].[Invoice]
		SET [sync_success_utc] = @sync_success_utc
			,[sync_attempt_utc] = NULL
			,[sync_invalid_utc] = NULL
			,[sync_log] = @sync_log
		WHERE [invoice_id] = @invoice_id
		AND [sync_success_utc] IS NULL
		AND (([sync_invalid_utc] IS NULL) OR ([sync_invalid_utc] <= @sync_success_utc))
	END
	ELSE
	BEGIN
		-- ON FAILED, SET SYNCH "ATTEMPT" DATE
		UPDATE [dbo].[Invoice]
		SET [sync_attempt_utc] = GETUTCDATE()
			,[sync_log] = @sync_log
		WHERE [invoice_id] = @invoice_id
		AND [sync_success_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spInvoice_HydrateSyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50) -- not used yet
AS
  SELECT [invoice_id]
  FROM [dbo].[Invoice]
  WHERE [sync_hydrate_utc] IS NULL
  ORDER BY [sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spInvoice_HydrateSyncUpdate]  
	 @invoice_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_hydrate_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)   -- not used yet
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNC DATE
		UPDATE [dbo].[Invoice]
		SET [sync_hydrate_utc] = @sync_hydrate_utc
		WHERE [invoice_id] = @invoice_id
		AND [sync_hydrate_utc] IS NULL
	END
	ELSE
	BEGIN
		-- ON FAILED, ADD TO LOG
		UPDATE [dbo].[Invoice]
		SET [sync_log] = @sync_log
		WHERE [invoice_id] = @invoice_id
		AND [sync_hydrate_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spPayment_SyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50)
AS
  SELECT [payment_id]
  FROM [dbo].[Payment]
  WHERE [sync_success_utc] IS NULL OR [deleted_utc] > [sync_success_utc]  OR [updated_utc] > [sync_success_utc]
  AND ISNULL([sync_agent],'') = ISNULL(@sync_agent,'')
  ORDER BY  -- oldest attempt, not attempted, failed -> then by change date  
	CASE WHEN NOT [sync_attempt_utc] IS NULL AND DATEDIFF(second,[sync_attempt_utc], GETUTCDATE()) > @allowableSecondsToProcessIndex  
			THEN 0 -- oldest in queue
		WHEN [sync_attempt_utc] IS NULL 
			THEN 1  -- synch is null , freshly invalidated 
		ELSE  2-- recently failed
	END asc
	,[sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spPayment_SyncUpdate]  
	 @payment_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_success_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)  
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNCH DATE
		UPDATE [dbo].[Payment]
		SET [sync_success_utc] = @sync_success_utc
			,[sync_attempt_utc] = NULL
			,[sync_invalid_utc] = NULL
			,[sync_log] = @sync_log
		WHERE [payment_id] = @payment_id
		AND [sync_success_utc] IS NULL
		AND (([sync_invalid_utc] IS NULL) OR ([sync_invalid_utc] <= @sync_success_utc))
	END
	ELSE
	BEGIN
		-- ON FAILED, SET SYNCH "ATTEMPT" DATE
		UPDATE [dbo].[Payment]
		SET [sync_attempt_utc] = GETUTCDATE()
			,[sync_log] = @sync_log
		WHERE [payment_id] = @payment_id
		AND [sync_success_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spPayment_HydrateSyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50) -- not used yet
AS
  SELECT [payment_id]
  FROM [dbo].[Payment]
  WHERE [sync_hydrate_utc] IS NULL
  ORDER BY [sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spPayment_HydrateSyncUpdate]  
	 @payment_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_hydrate_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)   -- not used yet
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNC DATE
		UPDATE [dbo].[Payment]
		SET [sync_hydrate_utc] = @sync_hydrate_utc
		WHERE [payment_id] = @payment_id
		AND [sync_hydrate_utc] IS NULL
	END
	ELSE
	BEGIN
		-- ON FAILED, ADD TO LOG
		UPDATE [dbo].[Payment]
		SET [sync_log] = @sync_log
		WHERE [payment_id] = @payment_id
		AND [sync_hydrate_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spShipment_SyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50)
AS
  SELECT [shipment_id]
  FROM [dbo].[Shipment]
  WHERE [sync_success_utc] IS NULL OR [deleted_utc] > [sync_success_utc]  OR [updated_utc] > [sync_success_utc]
  AND ISNULL([sync_agent],'') = ISNULL(@sync_agent,'')
  ORDER BY  -- oldest attempt, not attempted, failed -> then by change date  
	CASE WHEN NOT [sync_attempt_utc] IS NULL AND DATEDIFF(second,[sync_attempt_utc], GETUTCDATE()) > @allowableSecondsToProcessIndex  
			THEN 0 -- oldest in queue
		WHEN [sync_attempt_utc] IS NULL 
			THEN 1  -- synch is null , freshly invalidated 
		ELSE  2-- recently failed
	END asc
	,[sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spShipment_SyncUpdate]  
	 @shipment_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_success_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)  
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNCH DATE
		UPDATE [dbo].[Shipment]
		SET [sync_success_utc] = @sync_success_utc
			,[sync_attempt_utc] = NULL
			,[sync_invalid_utc] = NULL
			,[sync_log] = @sync_log
		WHERE [shipment_id] = @shipment_id
		AND [sync_success_utc] IS NULL
		AND (([sync_invalid_utc] IS NULL) OR ([sync_invalid_utc] <= @sync_success_utc))
	END
	ELSE
	BEGIN
		-- ON FAILED, SET SYNCH "ATTEMPT" DATE
		UPDATE [dbo].[Shipment]
		SET [sync_attempt_utc] = GETUTCDATE()
			,[sync_log] = @sync_log
		WHERE [shipment_id] = @shipment_id
		AND [sync_success_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spShipment_HydrateSyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50) -- not used yet
AS
  SELECT [shipment_id]
  FROM [dbo].[Shipment]
  WHERE [sync_hydrate_utc] IS NULL
  ORDER BY [sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spShipment_HydrateSyncUpdate]  
	 @shipment_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_hydrate_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)   -- not used yet
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNC DATE
		UPDATE [dbo].[Shipment]
		SET [sync_hydrate_utc] = @sync_hydrate_utc
		WHERE [shipment_id] = @shipment_id
		AND [sync_hydrate_utc] IS NULL
	END
	ELSE
	BEGIN
		-- ON FAILED, ADD TO LOG
		UPDATE [dbo].[Shipment]
		SET [sync_log] = @sync_log
		WHERE [shipment_id] = @shipment_id
		AND [sync_hydrate_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spPaymentTransaction_SyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50)
AS
  SELECT [paymenttransaction_id]
  FROM [dbo].[PaymentTransaction]
  WHERE [sync_success_utc] IS NULL OR [deleted_utc] > [sync_success_utc]  OR [updated_utc] > [sync_success_utc]
  AND ISNULL([sync_agent],'') = ISNULL(@sync_agent,'')
  ORDER BY  -- oldest attempt, not attempted, failed -> then by change date  
	CASE WHEN NOT [sync_attempt_utc] IS NULL AND DATEDIFF(second,[sync_attempt_utc], GETUTCDATE()) > @allowableSecondsToProcessIndex  
			THEN 0 -- oldest in queue
		WHEN [sync_attempt_utc] IS NULL 
			THEN 1  -- synch is null , freshly invalidated 
		ELSE  2-- recently failed
	END asc
	,[sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spPaymentTransaction_SyncUpdate]  
	 @paymenttransaction_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_success_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)  
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNCH DATE
		UPDATE [dbo].[PaymentTransaction]
		SET [sync_success_utc] = @sync_success_utc
			,[sync_attempt_utc] = NULL
			,[sync_invalid_utc] = NULL
			,[sync_log] = @sync_log
		WHERE [paymenttransaction_id] = @paymenttransaction_id
		AND [sync_success_utc] IS NULL
		AND (([sync_invalid_utc] IS NULL) OR ([sync_invalid_utc] <= @sync_success_utc))
	END
	ELSE
	BEGIN
		-- ON FAILED, SET SYNCH "ATTEMPT" DATE
		UPDATE [dbo].[PaymentTransaction]
		SET [sync_attempt_utc] = GETUTCDATE()
			,[sync_log] = @sync_log
		WHERE [paymenttransaction_id] = @paymenttransaction_id
		AND [sync_success_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spPaymentTransaction_HydrateSyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50) -- not used yet
AS
  SELECT [paymenttransaction_id]
  FROM [dbo].[PaymentTransaction]
  WHERE [sync_hydrate_utc] IS NULL
  ORDER BY [sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spPaymentTransaction_HydrateSyncUpdate]  
	 @paymenttransaction_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_hydrate_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)   -- not used yet
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNC DATE
		UPDATE [dbo].[PaymentTransaction]
		SET [sync_hydrate_utc] = @sync_hydrate_utc
		WHERE [paymenttransaction_id] = @paymenttransaction_id
		AND [sync_hydrate_utc] IS NULL
	END
	ELSE
	BEGIN
		-- ON FAILED, ADD TO LOG
		UPDATE [dbo].[PaymentTransaction]
		SET [sync_log] = @sync_log
		WHERE [paymenttransaction_id] = @paymenttransaction_id
		AND [sync_hydrate_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spPaymentDetail_SyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50)
AS
  SELECT [paymentdetail_id]
  FROM [dbo].[PaymentDetail]
  WHERE [sync_success_utc] IS NULL OR [deleted_utc] > [sync_success_utc]  OR [updated_utc] > [sync_success_utc]
  AND ISNULL([sync_agent],'') = ISNULL(@sync_agent,'')
  ORDER BY  -- oldest attempt, not attempted, failed -> then by change date  
	CASE WHEN NOT [sync_attempt_utc] IS NULL AND DATEDIFF(second,[sync_attempt_utc], GETUTCDATE()) > @allowableSecondsToProcessIndex  
			THEN 0 -- oldest in queue
		WHEN [sync_attempt_utc] IS NULL 
			THEN 1  -- synch is null , freshly invalidated 
		ELSE  2-- recently failed
	END asc
	,[sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spPaymentDetail_SyncUpdate]  
	 @paymentdetail_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_success_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)  
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNCH DATE
		UPDATE [dbo].[PaymentDetail]
		SET [sync_success_utc] = @sync_success_utc
			,[sync_attempt_utc] = NULL
			,[sync_invalid_utc] = NULL
			,[sync_log] = @sync_log
		WHERE [paymentdetail_id] = @paymentdetail_id
		AND [sync_success_utc] IS NULL
		AND (([sync_invalid_utc] IS NULL) OR ([sync_invalid_utc] <= @sync_success_utc))
	END
	ELSE
	BEGIN
		-- ON FAILED, SET SYNCH "ATTEMPT" DATE
		UPDATE [dbo].[PaymentDetail]
		SET [sync_attempt_utc] = GETUTCDATE()
			,[sync_log] = @sync_log
		WHERE [paymentdetail_id] = @paymentdetail_id
		AND [sync_success_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spPaymentDetail_HydrateSyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50) -- not used yet
AS
  SELECT [paymentdetail_id]
  FROM [dbo].[PaymentDetail]
  WHERE [sync_hydrate_utc] IS NULL
  ORDER BY [sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spPaymentDetail_HydrateSyncUpdate]  
	 @paymentdetail_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_hydrate_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)   -- not used yet
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNC DATE
		UPDATE [dbo].[PaymentDetail]
		SET [sync_hydrate_utc] = @sync_hydrate_utc
		WHERE [paymentdetail_id] = @paymentdetail_id
		AND [sync_hydrate_utc] IS NULL
	END
	ELSE
	BEGIN
		-- ON FAILED, ADD TO LOG
		UPDATE [dbo].[PaymentDetail]
		SET [sync_log] = @sync_log
		WHERE [paymentdetail_id] = @paymentdetail_id
		AND [sync_hydrate_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spAccount_SyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50)
AS
  SELECT [account_id]
  FROM [dbo].[Account]
  WHERE [sync_success_utc] IS NULL OR [deleted_utc] > [sync_success_utc]  OR [updated_utc] > [sync_success_utc]
  AND ISNULL([sync_agent],'') = ISNULL(@sync_agent,'')
  ORDER BY  -- oldest attempt, not attempted, failed -> then by change date  
	CASE WHEN NOT [sync_attempt_utc] IS NULL AND DATEDIFF(second,[sync_attempt_utc], GETUTCDATE()) > @allowableSecondsToProcessIndex  
			THEN 0 -- oldest in queue
		WHEN [sync_attempt_utc] IS NULL 
			THEN 1  -- synch is null , freshly invalidated 
		ELSE  2-- recently failed
	END asc
	,[sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spAccount_SyncUpdate]  
	 @account_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_success_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)  
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNCH DATE
		UPDATE [dbo].[Account]
		SET [sync_success_utc] = @sync_success_utc
			,[sync_attempt_utc] = NULL
			,[sync_invalid_utc] = NULL
			,[sync_log] = @sync_log
		WHERE [account_id] = @account_id
		AND [sync_success_utc] IS NULL
		AND (([sync_invalid_utc] IS NULL) OR ([sync_invalid_utc] <= @sync_success_utc))
	END
	ELSE
	BEGIN
		-- ON FAILED, SET SYNCH "ATTEMPT" DATE
		UPDATE [dbo].[Account]
		SET [sync_attempt_utc] = GETUTCDATE()
			,[sync_log] = @sync_log
		WHERE [account_id] = @account_id
		AND [sync_success_utc] IS NULL
	END  
END

GO

CREATE PROCEDURE [dbo].[spAccount_HydrateSyncGetInvalid]
	@allowableSecondsToProcessIndex int
    ,@sync_agent nvarchar(50) -- not used yet
AS
  SELECT [account_id]
  FROM [dbo].[Account]
  WHERE [sync_hydrate_utc] IS NULL
  ORDER BY [sync_invalid_utc] asc

GO

CREATE PROCEDURE [dbo].[spAccount_HydrateSyncUpdate]  
	 @account_id uniqueidentifier,  
	 @sync_success bit,  
	 @sync_hydrate_utc datetimeoffset(0),  
	 @sync_log nvarchar(MAX)   -- not used yet
AS  
BEGIN 
	IF (@sync_success = 1)   
	BEGIN  
		-- ON SUCCESSFUL, SET SYNC DATE
		UPDATE [dbo].[Account]
		SET [sync_hydrate_utc] = @sync_hydrate_utc
		WHERE [account_id] = @account_id
		AND [sync_hydrate_utc] IS NULL
	END
	ELSE
	BEGIN
		-- ON FAILED, ADD TO LOG
		UPDATE [dbo].[Account]
		SET [sync_log] = @sync_log
		WHERE [account_id] = @account_id
		AND [sync_hydrate_utc] IS NULL
	END  
END

GO

-- <Procedures> --------------------------------------------------------------------


-- <Foreign Keys> --------------------------------------------------------------------

ALTER TABLE [dbo].[Subscription] WITH CHECK ADD  CONSTRAINT [FK_Subscription_Brand_brand_id] FOREIGN KEY([brand_id])
REFERENCES [dbo].[Brand] ([brand_id])
GO

ALTER TABLE [dbo].[PaymentDetail] WITH CHECK ADD  CONSTRAINT [FK_PaymentDetail_Account_account_id] FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([account_id])
GO

ALTER TABLE [dbo].[PaymentTransaction] WITH CHECK ADD  CONSTRAINT [FK_PaymentTransaction_Order_order_id] FOREIGN KEY([order_id])
REFERENCES [dbo].[Order] ([order_id])
GO

ALTER TABLE [dbo].[PaymentTransaction] WITH CHECK ADD  CONSTRAINT [FK_PaymentTransaction_Payment_payment_id] FOREIGN KEY([payment_id])
REFERENCES [dbo].[Payment] ([payment_id])
GO

ALTER TABLE [dbo].[Shipment] WITH CHECK ADD  CONSTRAINT [FK_Shipment_Order_order_id] FOREIGN KEY([order_id])
REFERENCES [dbo].[Order] ([order_id])
GO

ALTER TABLE [dbo].[Payment] WITH CHECK ADD  CONSTRAINT [FK_Payment_Order_order_id] FOREIGN KEY([order_id])
REFERENCES [dbo].[Order] ([order_id])
GO

ALTER TABLE [dbo].[Invoice] WITH CHECK ADD  CONSTRAINT [FK_Invoice_Order_order_id] FOREIGN KEY([order_id])
REFERENCES [dbo].[Order] ([order_id])
GO

ALTER TABLE [dbo].[Invoice] WITH CHECK ADD  CONSTRAINT [FK_Invoice_Asset_asset_id] FOREIGN KEY([asset_id])
REFERENCES [dbo].[Asset] ([asset_id])
GO

ALTER TABLE [dbo].[LineItem] WITH CHECK ADD  CONSTRAINT [FK_LineItem_Order_order_id] FOREIGN KEY([order_id])
REFERENCES [dbo].[Order] ([order_id])
GO

ALTER TABLE [dbo].[LineItem] WITH CHECK ADD  CONSTRAINT [FK_LineItem_Listing_listing_id] FOREIGN KEY([listing_id])
REFERENCES [dbo].[Listing] ([listing_id])
GO

ALTER TABLE [dbo].[Order] WITH CHECK ADD  CONSTRAINT [FK_Order_Account_account_id] FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([account_id])
GO

ALTER TABLE [dbo].[Order] WITH CHECK ADD  CONSTRAINT [FK_Order_Invoice_invoice_id] FOREIGN KEY([invoice_id])
REFERENCES [dbo].[Invoice] ([invoice_id])
GO

ALTER TABLE [dbo].[Order] WITH CHECK ADD  CONSTRAINT [FK_Order_Payment_payment_id] FOREIGN KEY([payment_id])
REFERENCES [dbo].[Payment] ([payment_id])
GO

ALTER TABLE [dbo].[Order] WITH CHECK ADD  CONSTRAINT [FK_Order_Shipment_shipment_id] FOREIGN KEY([shipment_id])
REFERENCES [dbo].[Shipment] ([shipment_id])
GO

ALTER TABLE [dbo].[Listing] WITH CHECK ADD  CONSTRAINT [FK_Listing_Brand_brand_id] FOREIGN KEY([brand_id])
REFERENCES [dbo].[Brand] ([brand_id])
GO

ALTER TABLE [dbo].[Listing] WITH CHECK ADD  CONSTRAINT [FK_Listing_Product_product_id] FOREIGN KEY([product_id])
REFERENCES [dbo].[Product] ([product_id])
GO

ALTER TABLE [dbo].[Listing] WITH CHECK ADD  CONSTRAINT [FK_Listing_Promotion_promotion_id] FOREIGN KEY([promotion_id])
REFERENCES [dbo].[Promotion] ([promotion_id])
GO

ALTER TABLE [dbo].[Product] WITH CHECK ADD  CONSTRAINT [FK_Product_Brand_brand_id] FOREIGN KEY([brand_id])
REFERENCES [dbo].[Brand] ([brand_id])
GO

-- </Foreign Keys> --------------------------------------------------------------------


-- <Unique Keys> --------------------------------------------------------------------


IF OBJECT_ID('dbo.UK_account_key', 'UQ') IS NOT NULL BEGIN -- multiple passes because of script limitations, thats fine. :)
	ALTER TABLE [dbo].[Account] 
		DROP CONSTRAINT UK_account_key
END
ALTER TABLE [dbo].[Account] 
   ADD CONSTRAINT UK_account_key UNIQUE ([api_key]); 
GO

-- </Unique Keys> --------------------------------------------------------------------


