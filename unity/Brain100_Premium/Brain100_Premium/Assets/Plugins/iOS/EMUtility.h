//
//  EMUtility.h
//  EasyMobile
//
//  Copyright © 2017 SgLib Games. All rights reserved.
//

#import <StoreKit/StoreKit.h>
#import <UIKit/UIKit.h>

@interface EMUtility : NSObject

#pragma mark - ObjC stuff
+ (BOOL)isBuiltinRequestReviewAvail;
+ (void)requestReview;

#pragma mark - C API
typedef struct ShareData
{
    char *text;
    char *url;
    char *image;
    char *subject;
} ShareData;

bool _IsBuiltinRequestReviewAvail();
void _RequestReview();
void _Share(const ShareData *data);

@end
