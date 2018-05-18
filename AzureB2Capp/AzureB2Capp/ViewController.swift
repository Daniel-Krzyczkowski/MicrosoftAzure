//
//  ViewController.swift
//  AzureB2Capp
//
//  Created by Daniel Krzyczkowski on 18/05/2018.
//  Copyright © 2018 devisland. All rights reserved.
//

import Foundation
import UIKit
import MSAL

class ViewController: UIViewController, URLSessionDelegate
{
    var accessToken = String()
    var applicationContext = MSALPublicClientApplication.init()
    let kScopes: [String] = ["https://devisland.onmicrosoft.com/api/user_impersonation"]
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        do {
            self.applicationContext = try MSALPublicClientApplication.init(clientId: AzureAdB2CConfig.ClientID, authority: AzureAdB2CConfig.Authority)
        } catch {
            print("Unable to create Application Context. Error: \(error)")
        }
    }

    @IBAction func Login(_ sender: UIButton)
    {
        do
        {
            self.applicationContext.acquireToken(forScopes: self.kScopes) { (result, error) in
                if error == nil {
                    self.accessToken = (result?.accessToken)!
                    print("Access token is \(self.accessToken)")
                    //                        self.signoutButton.isEnabled = true;
                    
                } else  {
                    print("Could not acquire token: \(error ?? "No error information" as! Error)")
                }
            }
        }
    }
    
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
}

