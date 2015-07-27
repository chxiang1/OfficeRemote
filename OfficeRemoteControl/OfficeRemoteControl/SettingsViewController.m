//
//  ViewController.m
//  OfficeRemoteControl
//
//  Created by Chaos Xiang on 5/17/15.
//  Copyright (c) 2015 Chaos Xiang. All rights reserved.
//

#import "SettingsViewController.h"

@interface SettingsViewController ()

@end

@implementation SettingsViewController

@synthesize textBoxIP;
@synthesize textBoxPort;

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view, typically from a nib.
    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
    NSString *serviceIP = [userDefaults objectForKey:SERVICE_IP];
    NSString *servicePort = [userDefaults objectForKey:SERVICE_PORT];
    
    textBoxIP.text = serviceIP;
    textBoxPort.text = servicePort;
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (IBAction)update:(id)sender {
    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
    NSString *serviceIP = textBoxIP.text;
    NSString *servicePort = textBoxPort.text;
    
    [userDefaults setObject:serviceIP forKey:SERVICE_IP];
    [userDefaults setObject:servicePort forKey:SERVICE_PORT];
    [userDefaults synchronize];
    
    [[[UIApplication sharedApplication] keyWindow] endEditing:YES];
}

@end
