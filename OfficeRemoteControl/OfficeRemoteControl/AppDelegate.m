//
//  AppDelegate.m
//  OfficeRemoteControl
//
//  Created by Chaos Xiang on 5/17/15.
//  Copyright (c) 2015 Chaos Xiang. All rights reserved.
//

#import "AppDelegate.h"
#import "SettingsViewController.h"

@interface AppDelegate ()

@end

@implementation AppDelegate

BOOL isPlaying = FALSE;

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
    // Override point for customization after application launch.
    return YES;
}

- (void)application:(UIApplication *)application handleWatchKitExtensionRequest:(NSDictionary *)userInfo reply:(void (^)(NSDictionary *))reply {
    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
    NSString *serviceIP = [userDefaults objectForKey:SERVICE_IP];
    NSString *servicePort = [userDefaults objectForKey:SERVICE_PORT];
    
    NSString *urlString = [NSString stringWithFormat:@"http://%@:%@/Handlers/OfficeRemoteProxy.ashx", serviceIP, servicePort];
    
    if ([[userInfo objectForKey:@"action"] isEqualToString:@"left"]) {
        urlString = [urlString stringByAppendingString: @"?type=swipe&direction=left"];
    }
    else if ([[userInfo objectForKey:@"action"] isEqualToString:@"right"]) {
        urlString = [urlString stringByAppendingString: @"?type=swipe&direction=right"];
    }
    else if ([[userInfo objectForKey:@"action"] isEqualToString:@"toggle"]) {
        if (isPlaying) {
            urlString = [urlString stringByAppendingString: @"?type=stop"];
            isPlaying = FALSE;
        }
        else {
            urlString = [urlString stringByAppendingString: @"?type=run"];
            isPlaying = TRUE;
        }
    }
    urlString = [urlString stringByAppendingString: @"&offset_x=0&offset_y=0"];
    
    NSURL *url = [NSURL URLWithString:urlString];
    NSURLRequest *request = [NSURLRequest requestWithURL:url];
    NSOperationQueue *queue = [[NSOperationQueue alloc] init];
    
    [NSURLConnection sendAsynchronousRequest:request queue:queue completionHandler:^(NSURLResponse *response, NSData *data, NSError *connectionError) {
        // Callback
        if (connectionError) {
            NSLog(@"AsynchronousRequest1 get data is OK  on thread %@!!",[NSThread currentThread]);
        }
        else{
            NSLog(@" statusCode is %ld on thread %@",(long)[(NSHTTPURLResponse*)response  statusCode],[NSThread currentThread]);
        }
    }];
    
    NSDictionary *response = @{@"response" : @"OK"};
    reply(response);
}

- (void)applicationWillResignActive:(UIApplication *)application {
    // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
    // Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
}

- (void)applicationDidEnterBackground:(UIApplication *)application {
    // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later.
    // If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
}

- (void)applicationWillEnterForeground:(UIApplication *)application {
    // Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
}

- (void)applicationDidBecomeActive:(UIApplication *)application {
    // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
}

- (void)applicationWillTerminate:(UIApplication *)application {
    // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
}

@end
