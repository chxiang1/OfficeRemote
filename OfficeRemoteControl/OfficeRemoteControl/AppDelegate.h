//
//  AppDelegate.h
//  OfficeRemoteControl
//
//  Created by Chaos Xiang on 5/17/15.
//  Copyright (c) 2015 Chaos Xiang. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface AppDelegate : UIResponder <UIApplicationDelegate>

@property (strong, nonatomic) UIWindow *window;
@property (strong, nonatomic) NSMutableString *serviceIP;
@property (strong, nonatomic) NSMutableString *servicePort;

@end

