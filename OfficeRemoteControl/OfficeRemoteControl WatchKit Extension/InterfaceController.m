//
//  InterfaceController.m
//  OfficeRemoteControl WatchKit Extension
//
//  Created by Chaos Xiang on 5/17/15.
//  Copyright (c) 2015 Chaos Xiang. All rights reserved.
//

#import "InterfaceController.h"
#import "SettingsViewController.h"


@interface InterfaceController()
@property (weak, nonatomic) IBOutlet WKInterfaceButton *previousButton;
@property (weak, nonatomic) IBOutlet WKInterfaceButton *nextButton;
@property (weak, nonatomic) IBOutlet WKInterfaceButton *playStopButton;

@end


@implementation InterfaceController

- (IBAction)previousButtonClicked:(id)sender{
    [self sendCommand:@"left"];
}

- (IBAction)nextButtonClicked {
    [self sendCommand:@"right"];
}

- (IBAction)toggleButtonClicked {
    [self sendCommand:@"toggle"];
}

- (void)sendCommand:(NSString*)command {
    [InterfaceController openParentApplication:@{@"action": command} reply:^(NSDictionary *replyInfo, NSError *error) {
        if (error) {
            NSLog(@"%@", error);
        }
    }];
}

- (void)awakeWithContext:(id)context {
    [super awakeWithContext:context];

    // Configure interface objects here.
}

- (void)willActivate {
    // This method is called when watch view controller is about to be visible to user
    [super willActivate];
}

- (void)didDeactivate {
    // This method is called when watch view controller is no longer visible
    [super didDeactivate];
}

- (IBAction)previousButtonClicked {
}
@end



