//
//  AzureAdB2CConfig.swift
//  AzureB2Capp
//
//  Created by Daniel Krzyczkowski on 18/05/2018.
//  Copyright © 2018 devisland. All rights reserved.
//

import Foundation

struct AzureAdB2CConfig
{
    
    static var ClientID: String
    {
        get{
            return "<<APP ID HERE>>"
        }
    }
    
    static var Tenant: String
    {
        get{
            return "devisland.onmicrosoft.com"
        }
    }
    
    static var SignUpAndInPolicy: String
    {
        get{
            return "B2C_1_SignUpAndInPolicy"
        }
    }
    
    static var Authority: String
    {
        get{
            return "https://login.microsoftonline.com/tfp/\(Tenant)/\(SignUpAndInPolicy)/v2.0/"
        }
    }
}
