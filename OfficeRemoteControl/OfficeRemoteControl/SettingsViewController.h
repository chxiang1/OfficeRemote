//
//  ViewController.h
//  OfficeRemoteControl
//
//  Created by Chaos Xiang on 5/17/15.
//  Copyright (c) 2015 Chaos Xiang. All rights reserved.
//

#import <UIKit/UIKit.h>
#define SERVICE_IP @"ServiceIP"
#define SERVICE_PORT @"ServicePort"

@interface SettingsViewController : UIViewController

@property (strong, nonatomic) IBOutlet UITextField *textBoxIP;
@property (strong, nonatomic) IBOutlet UITextField *textBoxPort;

@end

