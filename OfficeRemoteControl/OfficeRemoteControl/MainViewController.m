//
//  MainViewController.m
//  OfficeRemoteControl
//
//  Created by Chaos Xiang on 5/25/15.
//  Copyright (c) 2015 Chaos Xiang. All rights reserved.
//

#import "MainViewController.h"
#import "SettingsViewController.h"

@interface MainViewController ()

@property (strong, nonatomic) IBOutlet UIWebView *webView;

@end

@implementation MainViewController

@synthesize webView;

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)viewDidAppear:(BOOL)animated
{
    [super viewDidAppear:animated];
    
    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
    NSString *serviceIP = [userDefaults objectForKey:SERVICE_IP];
    NSString *servicePort = [userDefaults objectForKey:SERVICE_PORT];
    NSString *serviceURL = [NSString stringWithFormat:@"http://%@:%@", serviceIP, servicePort];
    [webView loadRequest:[NSURLRequest requestWithURL:[NSURL URLWithString:serviceURL]]];
}

/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/

@end
