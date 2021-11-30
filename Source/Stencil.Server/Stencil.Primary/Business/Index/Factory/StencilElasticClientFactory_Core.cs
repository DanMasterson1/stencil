using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stencil.SDK.Models;
using sdk = Stencil.SDK.Models;

namespace Stencil.Primary.Business.Index
{
    public partial class StencilElasticClientFactory
    {
        partial void MapIndexModels(CreateIndexDescriptor indexer)
        {
            indexer.Mappings(mp => mp.Map<sdk.Brand>(DocumentNames.Brand, p => p
                .AutoMap()
                .Properties(props => props
                    .String(s => s
                        .Name(n => n.brand_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(m => m
                        .Name(t => t.product_count)
                        .Fields(f => f
                                .String(s => s.Name(n => n.product_count)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.product_count.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.listing_count)
                        .Fields(f => f
                                .String(s => s.Name(n => n.listing_count)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.listing_count.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.brand_name)
                        .Fields(f => f
                                .String(s => s.Name(n => n.brand_name)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.brand_name.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    )
                )
            ));
            indexer.Mappings(mp => mp.Map<sdk.Product>(DocumentNames.Product, p => p
                .AutoMap()
                .Properties(props => props
                    .String(s => s
                        .Name(n => n.product_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.brand_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(m => m
                        .Name(t => t.brand_name)
                        .Fields(f => f
                                .String(s => s.Name(n => n.brand_name)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.brand_name.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.product_name)
                        .Fields(f => f
                                .String(s => s.Name(n => n.product_name)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.product_name.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.baseprice)
                        .Fields(f => f
                                .String(s => s.Name(n => n.baseprice)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.baseprice.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    )
                )
            ));
            indexer.Mappings(mp => mp.Map<sdk.Promotion>(DocumentNames.Promotion, p => p
                .AutoMap()
                .Properties(props => props
                    .String(s => s
                        .Name(n => n.promotion_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    )
                )
            ));
            indexer.Mappings(mp => mp.Map<sdk.Listing>(DocumentNames.Listing, p => p
                .AutoMap()
                .Properties(props => props
                    .String(s => s
                        .Name(n => n.listing_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.brand_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.product_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.promotion_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(m => m
                        .Name(t => t.listing_price)
                        .Fields(f => f
                                .String(s => s.Name(n => n.listing_price)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.listing_price.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.product_baseprice)
                        .Fields(f => f
                                .String(s => s.Name(n => n.product_baseprice)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.product_baseprice.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.brand_name)
                        .Fields(f => f
                                .String(s => s.Name(n => n.brand_name)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.brand_name.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.product_name)
                        .Fields(f => f
                                .String(s => s.Name(n => n.product_name)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.product_name.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.promotion_description)
                        .Fields(f => f
                                .String(s => s.Name(n => n.promotion_description)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.promotion_description.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    )
                )
            ));
            indexer.Mappings(mp => mp.Map<sdk.Order>(DocumentNames.Order, p => p
                .AutoMap()
                .Properties(props => props
                    .String(s => s
                        .Name(n => n.order_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.account_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.invoice_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.payment_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.shipment_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(m => m
                        .Name(t => t.order_total)
                        .Fields(f => f
                                .String(s => s.Name(n => n.order_total)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.order_total.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.created_utc)
                        .Fields(f => f
                                .String(s => s.Name(n => n.created_utc)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.created_utc.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.account_name)
                        .Fields(f => f
                                .String(s => s.Name(n => n.account_name)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.account_name.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.account_email)
                        .Fields(f => f
                                .String(s => s.Name(n => n.account_email)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.account_email.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.shipment_address)
                        .Fields(f => f
                                .String(s => s.Name(n => n.shipment_address)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.shipment_address.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.payment_cardtype)
                        .Fields(f => f
                                .String(s => s.Name(n => n.payment_cardtype)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.payment_cardtype.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.status)
                        .Fields(f => f
                                .String(s => s.Name(n => n.status)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.status.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.lineitem_count)
                        .Fields(f => f
                                .String(s => s.Name(n => n.lineitem_count)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.lineitem_count.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.products)
                        .Fields(f => f
                                .String(s => s.Name(n => n.products)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.products.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    )
                )
            ));
            indexer.Mappings(mp => mp.Map<sdk.LineItem>(DocumentNames.LineItem, p => p
                .AutoMap()
                .Properties(props => props
                    .String(s => s
                        .Name(n => n.lineitem_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.order_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.listing_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    )
                )
            ));
            indexer.Mappings(mp => mp.Map<sdk.Invoice>(DocumentNames.Invoice, p => p
                .AutoMap()
                .Properties(props => props
                    .String(s => s
                        .Name(n => n.invoice_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.order_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.asset_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    )
                )
            ));
            indexer.Mappings(mp => mp.Map<sdk.Payment>(DocumentNames.Payment, p => p
                .AutoMap()
                .Properties(props => props
                    .String(s => s
                        .Name(n => n.payment_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.order_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    )
                )
            ));
            indexer.Mappings(mp => mp.Map<sdk.Shipment>(DocumentNames.Shipment, p => p
                .AutoMap()
                .Properties(props => props
                    .String(s => s
                        .Name(n => n.shipment_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.order_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    )
                )
            ));
            indexer.Mappings(mp => mp.Map<sdk.PaymentTransaction>(DocumentNames.PaymentTransaction, p => p
                .AutoMap()
                .Properties(props => props
                    .String(s => s
                        .Name(n => n.paymenttransaction_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.order_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.payment_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    )
                )
            ));
            indexer.Mappings(mp => mp.Map<sdk.PaymentDetail>(DocumentNames.PaymentDetail, p => p
                .AutoMap()
                .Properties(props => props
                    .String(s => s
                        .Name(n => n.paymentdetail_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(s => s
                        .Name(n => n.account_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    )
                )
            ));
            indexer.Mappings(mp => mp.Map<sdk.Account>(DocumentNames.Account, p => p
                .AutoMap()
                .Properties(props => props
                    .String(s => s
                        .Name(n => n.account_id)
                        .Index(FieldIndexOption.NotAnalyzed)
                    ).String(m => m
                        .Name(t => t.email)
                        .Fields(f => f
                                .String(s => s.Name(n => n.email)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.email.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.first_name)
                        .Fields(f => f
                                .String(s => s.Name(n => n.first_name)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.first_name.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.last_name)
                        .Fields(f => f
                                .String(s => s.Name(n => n.last_name)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.last_name.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.last_login_utc)
                        .Fields(f => f
                                .String(s => s.Name(n => n.last_login_utc)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.last_login_utc.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    ).String(m => m
                        .Name(t => t.last_login_platform)
                        .Fields(f => f
                                .String(s => s.Name(n => n.last_login_platform)
                                .Index(FieldIndexOption.Analyzed))
                                .String(s => s
                                    .Name(n => n.last_login_platform.Suffix("sort"))
                                    .Analyzer("case_insensitive"))
                                
                        )
                    )
                )
            ));
            
        }
    }
}
