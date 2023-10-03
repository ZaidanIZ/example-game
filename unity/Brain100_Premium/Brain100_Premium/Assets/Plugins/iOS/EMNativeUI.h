//
//  EMNativeUI.h
//  EasyMobile
//
//  Copyright © 2017 SgLib Games. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

@interface EMNativeUI : NSObject

#pragma mark - ObjC stuff
+ (void)alertWithTitle: (NSString *)title message: (NSString*)msg button1: (NSString*)b1 button2: (NSString*)b2 button3: (NSString*)b3;
+ (void)alertWithTitle: (NSString *)title message: (NSString*)msg button1: (NSString*)b1 button2: (NSString*)b2;
+ (void)alertWithTitle: (NSString *)title message: (NSString*)msg button:(NSString*)b1;


#pragma mark - C API
void _AlertWithThreeButtons(const char *title, const char *message, const char *button1, const char *button2, const char *button3);
void _AlertWithTwoButtons(const char *title, const char *message, const char *button1, const char *button2);
void _Alert(const char* title, const char *message, const char *button);

@end
