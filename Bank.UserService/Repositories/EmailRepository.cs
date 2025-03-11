using Bank.UserService.Models;

namespace Bank.UserService.Repositories;

public enum EmailType
{
    UserActivateAccount,
    UserResetPassword
}

public interface IEmailRepository
{
    public Email Find(EmailType type);
}

public class EmailRepository : IEmailRepository
{
    private readonly Dictionary<EmailType, Email> m_Emails = new()
                                                             {
                                                                 {
                                                                     EmailType.UserActivateAccount, new Email()
                                                                                                    {
                                                                                                        Subject = "Activate Your Account",
                                                                                                        Body = """
                                                                                                               <!DOCTYPE html>
                                                                                                               <html xmlns:v="urn:schemas-microsoft-com:vml" xmlns:o="urn:schemas-microsoft-com:office:office">
                                                                                                               <head>
                                                                                                                <meta charset="UTF-8" />
                                                                                                                <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
                                                                                                                <!--[if !mso]><!-- -->
                                                                                                                <meta http-equiv="X-UA-Compatible" content="IE=edge" />
                                                                                                                <!--<![endif]-->
                                                                                                                <meta name="viewport" content="width=device-width, initial-scale=1.0" />
                                                                                                                <meta name="format-detection" content="telephone=no, date=no, address=no, email=no" />
                                                                                                                <meta name="x-apple-disable-message-reformatting" />
                                                                                                                <link href="https://fonts.googleapis.com/css?family=Montserrat:ital,wght@0,400;0,500;0,600" rel="stylesheet" />
                                                                                                                <title>Untitled</title>
                                                                                                                <!-- Made with Postcards Email Builder by Designmodo -->
                                                                                                                <style>
                                                                                                                html, body { margin: 0 !important; padding: 0 !important; min-height: 100% !important; width: 100% !important; -webkit-font-smoothing: antialiased; }
                                                                                                                        * { -ms-text-size-adjust: 100%; }
                                                                                                                        #outlook a { padding: 0; }
                                                                                                                        .ReadMsgBody, .ExternalClass { width: 100%; }
                                                                                                                        .ExternalClass, .ExternalClass p, .ExternalClass td, .ExternalClass div, .ExternalClass span, .ExternalClass font { line-height: 100%; }
                                                                                                                        table, td, th { mso-table-lspace: 0 !important; mso-table-rspace: 0 !important; border-collapse: collapse; }
                                                                                                                        u + .body table, u + .body td, u + .body th { will-change: transform; }
                                                                                                                        body, td, th, p, div, li, a, span { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-line-height-rule: exactly; }
                                                                                                                        img { border: 0; outline: 0; line-height: 100%; text-decoration: none; -ms-interpolation-mode: bicubic; }
                                                                                                                        a[x-apple-data-detectors] { color: inherit !important; text-decoration: none !important; }
                                                                                                                        .body .pc-project-body { background-color: transparent !important; }
                                                                                                                                
                                                                                                                
                                                                                                                        @media (min-width: 621px) {
                                                                                                                            .pc-lg-hide {  display: none; } 
                                                                                                                            .pc-lg-bg-img-hide { background-image: none !important; }
                                                                                                                        }
                                                                                                                </style>
                                                                                                                <style>
                                                                                                                @media (max-width: 620px) {
                                                                                                                .pc-project-body {min-width: 0px !important;}
                                                                                                                .pc-project-container {width: 100% !important;}
                                                                                                                .pc-sm-hide, .pc-w620-gridCollapsed-1 > tbody > tr > .pc-sm-hide {display: none !important;}
                                                                                                                .pc-sm-bg-img-hide {background-image: none !important;}
                                                                                                                .pc-w620-padding-25-35-0-35 {padding: 25px 35px 0px 35px !important;}
                                                                                                                .pc-w620-padding-15-35-0-35 {padding: 15px 35px 0px 35px !important;}
                                                                                                                .pc-w620-padding-15-30-15-30 {padding: 15px 30px 15px 30px !important;}
                                                                                                                .pc-w620-padding-10-35-10-35 {padding: 10px 35px 10px 35px !important;}
                                                                                                                
                                                                                                                .pc-w620-gridCollapsed-1 > tbody,.pc-w620-gridCollapsed-1 > tbody > tr,.pc-w620-gridCollapsed-1 > tr {display: inline-block !important;}
                                                                                                                .pc-w620-gridCollapsed-1.pc-width-fill > tbody,.pc-w620-gridCollapsed-1.pc-width-fill > tbody > tr,.pc-w620-gridCollapsed-1.pc-width-fill > tr {width: 100% !important;}
                                                                                                                .pc-w620-gridCollapsed-1.pc-w620-width-fill > tbody,.pc-w620-gridCollapsed-1.pc-w620-width-fill > tbody > tr,.pc-w620-gridCollapsed-1.pc-w620-width-fill > tr {width: 100% !important;}
                                                                                                                .pc-w620-gridCollapsed-1 > tbody > tr > td,.pc-w620-gridCollapsed-1 > tr > td {display: block !important;width: auto !important;padding-left: 0 !important;padding-right: 0 !important;margin-left: 0 !important;}
                                                                                                                .pc-w620-gridCollapsed-1.pc-width-fill > tbody > tr > td,.pc-w620-gridCollapsed-1.pc-width-fill > tr > td {width: 100% !important;}
                                                                                                                .pc-w620-gridCollapsed-1.pc-w620-width-fill > tbody > tr > td,.pc-w620-gridCollapsed-1.pc-w620-width-fill > tr > td {width: 100% !important;}
                                                                                                                .pc-w620-gridCollapsed-1 > tbody > .pc-grid-tr-first > .pc-grid-td-first,.pc-w620-gridCollapsed-1 > .pc-grid-tr-first > .pc-grid-td-first {padding-top: 0 !important;}
                                                                                                                .pc-w620-gridCollapsed-1 > tbody > .pc-grid-tr-last > .pc-grid-td-last,.pc-w620-gridCollapsed-1 > .pc-grid-tr-last > .pc-grid-td-last {padding-bottom: 0 !important;}
                                                                                                                
                                                                                                                .pc-w620-gridCollapsed-0 > tbody > .pc-grid-tr-first > td,.pc-w620-gridCollapsed-0 > .pc-grid-tr-first > td {padding-top: 0 !important;}
                                                                                                                .pc-w620-gridCollapsed-0 > tbody > .pc-grid-tr-last > td,.pc-w620-gridCollapsed-0 > .pc-grid-tr-last > td {padding-bottom: 0 !important;}
                                                                                                                .pc-w620-gridCollapsed-0 > tbody > tr > .pc-grid-td-first,.pc-w620-gridCollapsed-0 > tr > .pc-grid-td-first {padding-left: 0 !important;}
                                                                                                                .pc-w620-gridCollapsed-0 > tbody > tr > .pc-grid-td-last,.pc-w620-gridCollapsed-0 > tr > .pc-grid-td-last {padding-right: 0 !important;}
                                                                                                                
                                                                                                                .pc-w620-tableCollapsed-1 > tbody,.pc-w620-tableCollapsed-1 > tbody > tr,.pc-w620-tableCollapsed-1 > tr {display: block !important;}
                                                                                                                .pc-w620-tableCollapsed-1.pc-width-fill > tbody,.pc-w620-tableCollapsed-1.pc-width-fill > tbody > tr,.pc-w620-tableCollapsed-1.pc-width-fill > tr {width: 100% !important;}
                                                                                                                .pc-w620-tableCollapsed-1.pc-w620-width-fill > tbody,.pc-w620-tableCollapsed-1.pc-w620-width-fill > tbody > tr,.pc-w620-tableCollapsed-1.pc-w620-width-fill > tr {width: 100% !important;}
                                                                                                                .pc-w620-tableCollapsed-1 > tbody > tr > td,.pc-w620-tableCollapsed-1 > tr > td {display: block !important;width: auto !important;}
                                                                                                                .pc-w620-tableCollapsed-1.pc-width-fill > tbody > tr > td,.pc-w620-tableCollapsed-1.pc-width-fill > tr > td {width: 100% !important;box-sizing: border-box !important;}
                                                                                                                .pc-w620-tableCollapsed-1.pc-w620-width-fill > tbody > tr > td,.pc-w620-tableCollapsed-1.pc-w620-width-fill > tr > td {width: 100% !important;box-sizing: border-box !important;}
                                                                                                                }
                                                                                                                @media (max-width: 520px) {
                                                                                                                .pc-w520-padding-25-30-0-30 {padding: 25px 30px 0px 30px !important;}
                                                                                                                .pc-w520-padding-15-30-0-30 {padding: 15px 30px 0px 30px !important;}
                                                                                                                .pc-w520-padding-15-25-15-25 {padding: 15px 25px 15px 25px !important;}
                                                                                                                .pc-w520-padding-10-30-10-30 {padding: 10px 30px 10px 30px !important;}
                                                                                                                }
                                                                                                                </style>
                                                                                                                <!--[if !mso]><!-- -->
                                                                                                                <style>
                                                                                                                @font-face { font-family: 'Montserrat'; font-style: normal; font-weight: 600; src: url('https://fonts.gstatic.com/s/montserrat/v29/JTUHjIg1_i6t8kCHKm4532VJOt5-QNFgpCu173w3aXw.woff') format('woff'), url('https://fonts.gstatic.com/s/montserrat/v29/JTUHjIg1_i6t8kCHKm4532VJOt5-QNFgpCu173w3aXo.woff2') format('woff2'); } @font-face { font-family: 'Montserrat'; font-style: normal; font-weight: 400; src: url('https://fonts.gstatic.com/s/montserrat/v29/JTUHjIg1_i6t8kCHKm4532VJOt5-QNFgpCtr6Hw3aXw.woff') format('woff'), url('https://fonts.gstatic.com/s/montserrat/v29/JTUHjIg1_i6t8kCHKm4532VJOt5-QNFgpCtr6Hw3aXo.woff2') format('woff2'); } @font-face { font-family: 'Montserrat'; font-style: normal; font-weight: 500; src: url('https://fonts.gstatic.com/s/montserrat/v29/JTUHjIg1_i6t8kCHKm4532VJOt5-QNFgpCtZ6Hw3aXw.woff') format('woff'), url('https://fonts.gstatic.com/s/montserrat/v29/JTUHjIg1_i6t8kCHKm4532VJOt5-QNFgpCtZ6Hw3aXo.woff2') format('woff2'); }
                                                                                                                </style>
                                                                                                                <!--<![endif]-->
                                                                                                                <!--[if mso]>
                                                                                                                   <style type="text/css">
                                                                                                                       .pc-font-alt {
                                                                                                                           font-family: Arial, Helvetica, sans-serif !important;
                                                                                                                       }
                                                                                                                   </style>
                                                                                                                   <![endif]-->
                                                                                                                <!--[if gte mso 9]>
                                                                                                                   <xml>
                                                                                                                       <o:OfficeDocumentSettings>
                                                                                                                           <o:AllowPNG/>
                                                                                                                           <o:PixelsPerInch>96</o:PixelsPerInch>
                                                                                                                       </o:OfficeDocumentSettings>
                                                                                                                   </xml>
                                                                                                                   <![endif]-->
                                                                                                               </head>

                                                                                                               <body class="body pc-font-alt" style="width: 100% !important; min-height: 100% !important; margin: 0 !important; padding: 0 !important; line-height: 1.5; font-weight: normal; color: #2D3A41; mso-line-height-rule: exactly; -webkit-font-smoothing: antialiased; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; font-variant-ligatures: normal; text-rendering: optimizeLegibility; -moz-osx-font-smoothing: grayscale; background-color: #ffffff;" bgcolor="#ffffff">
                                                                                                                <table class="pc-project-body" style="table-layout: fixed; width: 100%; min-width: 600px; background-color: #ffffff;" bgcolor="#ffffff" border="0" cellspacing="0" cellpadding="0" role="presentation">
                                                                                                                 <tr>
                                                                                                                  <td align="center" valign="top">
                                                                                                                   <table class="pc-project-container" align="center" style="width: 600px; max-width: 600px;" border="0" cellpadding="0" cellspacing="0" role="presentation">
                                                                                                                    <tr>
                                                                                                                     <td style="padding: 20px 0px 20px 0px;" align="left" valign="top">
                                                                                                                      <table border="0" cellpadding="0" cellspacing="0" role="presentation" width="100%">
                                                                                                                       <tr>
                                                                                                                        <td valign="top">
                                                                                                                         <table width="100%" border="0" cellspacing="0" cellpadding="0" role="presentation">
                                                                                                                          <tr>
                                                                                                                           <td valign="top" class="pc-w520-padding-25-30-0-30 pc-w620-padding-25-35-0-35" style="padding: 32px 40px 0px 40px; height: unset; background-color: #ffffff;" bgcolor="#ffffff">
                                                                                                                            <table border="0" cellpadding="0" cellspacing="0" role="presentation" width="100%">
                                                                                                                             <tr>
                                                                                                                              <td valign="top" align="left">
                                                                                                                               <div class="pc-font-alt" style="text-decoration: none;">
                                                                                                                                <div style="font-size: 24px;line-height: 42px;text-align:center;text-align-last:center;color:#434343;font-style:normal;font-weight:600;letter-spacing:-0.2px;">
                                                                                                                                 <div><span style="font-family: 'Montserrat', Arial, Helvetica, sans-serif;">Activate Your Uwubankster Account</span>
                                                                                                                                 </div>
                                                                                                                                </div>
                                                                                                                               </div>
                                                                                                                              </td>
                                                                                                                             </tr>
                                                                                                                            </table>
                                                                                                                           </td>
                                                                                                                          </tr>
                                                                                                                         </table>
                                                                                                                        </td>
                                                                                                                       </tr>
                                                                                                                       <tr>
                                                                                                                        <td valign="top">
                                                                                                                         <table width="100%" border="0" cellspacing="0" cellpadding="0" role="presentation">
                                                                                                                          <tr>
                                                                                                                           <td valign="top" class="pc-w520-padding-15-30-0-30 pc-w620-padding-15-35-0-35" style="padding: 24px 40px 16px 40px; height: unset; background-color: #ffffff;" bgcolor="#ffffff">
                                                                                                                            <table border="0" cellpadding="0" cellspacing="0" role="presentation" width="100%">
                                                                                                                             <tr>
                                                                                                                              <td valign="top" align="left">
                                                                                                                               <div class="pc-font-alt" style="text-decoration: none;">
                                                                                                                                <div style="font-size: 18px;line-height: 24px;text-align:center;text-align-last:center;color:#434343;font-style:normal;font-weight:500;letter-spacing:-0.2px;">
                                                                                                                                 <div><span style="font-family: 'Montserrat', Arial, Helvetica, sans-serif;">Welcome to Uwubankster! We’re thrilled to have you join our community. To get started, please activate your account by clicking the button below:</span>
                                                                                                                                 </div>
                                                                                                                                </div>
                                                                                                                               </div>
                                                                                                                              </td>
                                                                                                                             </tr>
                                                                                                                            </table>
                                                                                                                           </td>
                                                                                                                          </tr>
                                                                                                                         </table>
                                                                                                                        </td>
                                                                                                                       </tr>
                                                                                                                       <tr>
                                                                                                                        <td valign="top">
                                                                                                                         <table width="100%" border="0" cellspacing="0" cellpadding="0" role="presentation">
                                                                                                                          <tr>
                                                                                                                           <td valign="top" class="pc-w520-padding-15-25-15-25 pc-w620-padding-15-30-15-30" style="padding: 16px 40px 16px 40px; height: unset; background-color: #ffffff;" bgcolor="#ffffff">
                                                                                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0" role="presentation">
                                                                                                                             <tr>
                                                                                                                              <th valign="top" align="center" style="text-align: center;">
                                                                                                                               <!--[if mso]>
                                                                                                                       <table border="0" cellpadding="0" cellspacing="0" role="presentation" align="center" style="border-collapse: separate; border-spacing: 0; margin-right: auto; margin-left: auto;">
                                                                                                                           <tr>
                                                                                                                               <td valign="middle" align="center" style="border-radius: 100px 100px 100px 100px; background-color: #1595e7; text-align:center; color: #ffffff; padding: 12px 32px 12px 32px; mso-padding-left-alt: 0; margin-left:32px;" bgcolor="#1595e7">
                                                                                                                                                   <a class="pc-font-alt" style="display: inline-block; text-decoration: none; font-family: 'Fira Sans', Arial, Helvetica, sans-serif; text-align: center;" href="{{0}}" target="_blank"><span style="font-size: 16px;line-height: 24px;color:#ffffff;letter-spacing:-0.2px;font-weight:500;font-style:normal;display:inline-block;"><span style="display:inline-block;"><span style="font-family: 'Montserrat', Arial, Helvetica, sans-serif;line-height: 150%;">Activate Account</span></span></span></a>
                                                                                                                                               </td>
                                                                                                                           </tr>
                                                                                                                       </table>
                                                                                                                       <![endif]-->
                                                                                                                               <!--[if !mso]><!-- -->
                                                                                                                               <a style="display: inline-block; box-sizing: border-box; border-radius: 100px 100px 100px 100px; background-color: #1595e7; padding: 12px 32px 12px 32px; font-family: 'Fira Sans', Arial, Helvetica, sans-serif; vertical-align: top; text-align: center; text-align-last: center; text-decoration: none; -webkit-text-size-adjust: none;" href="{{0}}" target="_blank"><span style="font-size: 16px;line-height: 24px;color:#ffffff;letter-spacing:-0.2px;font-weight:500;font-style:normal;display:inline-block;"><span style="display:inline-block;"><span style="font-family: 'Montserrat', Arial, Helvetica, sans-serif;line-height: 150%;">Activate Account</span></span></span></a>
                                                                                                                               <!--<![endif]-->
                                                                                                                              </th>
                                                                                                                             </tr>
                                                                                                                            </table>
                                                                                                                           </td>
                                                                                                                          </tr>
                                                                                                                         </table>
                                                                                                                        </td>
                                                                                                                       </tr>
                                                                                                                       <tr>
                                                                                                                        <td valign="top">
                                                                                                                         <table width="100%" border="0" cellspacing="0" cellpadding="0" role="presentation">
                                                                                                                          <tr>
                                                                                                                           <td valign="top" class="pc-w520-padding-10-30-10-30 pc-w620-padding-10-35-10-35" style="padding: 10px 40px 32px 40px; height: unset; background-color: #ffffff;" bgcolor="#ffffff">
                                                                                                                            <table border="0" cellpadding="0" cellspacing="0" role="presentation" width="100%">
                                                                                                                             <tr>
                                                                                                                              <td valign="top" align="left">
                                                                                                                               <div class="pc-font-alt" style="text-decoration: none;">
                                                                                                                                <div style="font-size: 15px;line-height: 21px;text-align:left;text-align-last:left;color:#333333;font-style:normal;font-weight:500;letter-spacing:-0.2px;">
                                                                                                                                 <div><span style="font-family: 'Montserrat', Arial, Helvetica, sans-serif;">Best regards,</span>
                                                                                                                                 </div>
                                                                                                                                 <div><span style="font-family: 'Montserrat', Arial, Helvetica, sans-serif;">The Uwubankster Team</span>
                                                                                                                                 </div>
                                                                                                                                </div>
                                                                                                                               </div>
                                                                                                                              </td>
                                                                                                                             </tr>
                                                                                                                            </table>
                                                                                                                           </td>
                                                                                                                          </tr>
                                                                                                                         </table>
                                                                                                                        </td>
                                                                                                                       </tr>
                                                                                                                      </table>
                                                                                                                     </td>
                                                                                                                    </tr>
                                                                                                                   </table>
                                                                                                                  </td>
                                                                                                                 </tr>
                                                                                                                </table>
                                                                                                               </body>

                                                                                                               </html>
                                                                                                               """
                                                                                                    }
                                                                 },
                                                                 {
                                                                     EmailType.UserResetPassword, new Email()
                                                                                                  {
                                                                                                      Subject = "Reset Your Password",
                                                                                                      Body = """
                                                                                                             <!DOCTYPE html>
                                                                                                             <html xmlns:v="urn:schemas-microsoft-com:vml" xmlns:o="urn:schemas-microsoft-com:office:office">
                                                                                                             <head>
                                                                                                              <meta charset="UTF-8" />
                                                                                                              <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
                                                                                                              <!--[if !mso]><!-- -->
                                                                                                              <meta http-equiv="X-UA-Compatible" content="IE=edge" />
                                                                                                              <!--<![endif]-->
                                                                                                              <meta name="viewport" content="width=device-width, initial-scale=1.0" />
                                                                                                              <meta name="format-detection" content="telephone=no, date=no, address=no, email=no" />
                                                                                                              <meta name="x-apple-disable-message-reformatting" />
                                                                                                              <link href="https://fonts.googleapis.com/css?family=Montserrat:ital,wght@0,400;0,500;0,600" rel="stylesheet" />
                                                                                                              <title>Untitled</title>
                                                                                                              <!-- Made with Postcards Email Builder by Designmodo -->
                                                                                                              <style>
                                                                                                              html, body { margin: 0 !important; padding: 0 !important; min-height: 100% !important; width: 100% !important; -webkit-font-smoothing: antialiased; }
                                                                                                                      * { -ms-text-size-adjust: 100%; }
                                                                                                                      #outlook a { padding: 0; }
                                                                                                                      .ReadMsgBody, .ExternalClass { width: 100%; }
                                                                                                                      .ExternalClass, .ExternalClass p, .ExternalClass td, .ExternalClass div, .ExternalClass span, .ExternalClass font { line-height: 100%; }
                                                                                                                      table, td, th { mso-table-lspace: 0 !important; mso-table-rspace: 0 !important; border-collapse: collapse; }
                                                                                                                      u + .body table, u + .body td, u + .body th { will-change: transform; }
                                                                                                                      body, td, th, p, div, li, a, span { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-line-height-rule: exactly; }
                                                                                                                      img { border: 0; outline: 0; line-height: 100%; text-decoration: none; -ms-interpolation-mode: bicubic; }
                                                                                                                      a[x-apple-data-detectors] { color: inherit !important; text-decoration: none !important; }
                                                                                                                      .body .pc-project-body { background-color: transparent !important; }
                                                                                                                              
                                                                                                              
                                                                                                                      @media (min-width: 621px) {
                                                                                                                          .pc-lg-hide {  display: none; } 
                                                                                                                          .pc-lg-bg-img-hide { background-image: none !important; }
                                                                                                                      }
                                                                                                              </style>
                                                                                                              <style>
                                                                                                              @media (max-width: 620px) {
                                                                                                              .pc-project-body {min-width: 0px !important;}
                                                                                                              .pc-project-container {width: 100% !important;}
                                                                                                              .pc-sm-hide, .pc-w620-gridCollapsed-1 > tbody > tr > .pc-sm-hide {display: none !important;}
                                                                                                              .pc-sm-bg-img-hide {background-image: none !important;}
                                                                                                              .pc-w620-padding-25-35-0-35 {padding: 25px 35px 0px 35px !important;}
                                                                                                              .pc-w620-padding-15-35-0-35 {padding: 15px 35px 0px 35px !important;}
                                                                                                              .pc-w620-padding-15-30-15-30 {padding: 15px 30px 15px 30px !important;}
                                                                                                              .pc-w620-padding-10-35-10-35 {padding: 10px 35px 10px 35px !important;}
                                                                                                              
                                                                                                              .pc-w620-gridCollapsed-1 > tbody,.pc-w620-gridCollapsed-1 > tbody > tr,.pc-w620-gridCollapsed-1 > tr {display: inline-block !important;}
                                                                                                              .pc-w620-gridCollapsed-1.pc-width-fill > tbody,.pc-w620-gridCollapsed-1.pc-width-fill > tbody > tr,.pc-w620-gridCollapsed-1.pc-width-fill > tr {width: 100% !important;}
                                                                                                              .pc-w620-gridCollapsed-1.pc-w620-width-fill > tbody,.pc-w620-gridCollapsed-1.pc-w620-width-fill > tbody > tr,.pc-w620-gridCollapsed-1.pc-w620-width-fill > tr {width: 100% !important;}
                                                                                                              .pc-w620-gridCollapsed-1 > tbody > tr > td,.pc-w620-gridCollapsed-1 > tr > td {display: block !important;width: auto !important;padding-left: 0 !important;padding-right: 0 !important;margin-left: 0 !important;}
                                                                                                              .pc-w620-gridCollapsed-1.pc-width-fill > tbody > tr > td,.pc-w620-gridCollapsed-1.pc-width-fill > tr > td {width: 100% !important;}
                                                                                                              .pc-w620-gridCollapsed-1.pc-w620-width-fill > tbody > tr > td,.pc-w620-gridCollapsed-1.pc-w620-width-fill > tr > td {width: 100% !important;}
                                                                                                              .pc-w620-gridCollapsed-1 > tbody > .pc-grid-tr-first > .pc-grid-td-first,.pc-w620-gridCollapsed-1 > .pc-grid-tr-first > .pc-grid-td-first {padding-top: 0 !important;}
                                                                                                              .pc-w620-gridCollapsed-1 > tbody > .pc-grid-tr-last > .pc-grid-td-last,.pc-w620-gridCollapsed-1 > .pc-grid-tr-last > .pc-grid-td-last {padding-bottom: 0 !important;}
                                                                                                              
                                                                                                              .pc-w620-gridCollapsed-0 > tbody > .pc-grid-tr-first > td,.pc-w620-gridCollapsed-0 > .pc-grid-tr-first > td {padding-top: 0 !important;}
                                                                                                              .pc-w620-gridCollapsed-0 > tbody > .pc-grid-tr-last > td,.pc-w620-gridCollapsed-0 > .pc-grid-tr-last > td {padding-bottom: 0 !important;}
                                                                                                              .pc-w620-gridCollapsed-0 > tbody > tr > .pc-grid-td-first,.pc-w620-gridCollapsed-0 > tr > .pc-grid-td-first {padding-left: 0 !important;}
                                                                                                              .pc-w620-gridCollapsed-0 > tbody > tr > .pc-grid-td-last,.pc-w620-gridCollapsed-0 > tr > .pc-grid-td-last {padding-right: 0 !important;}
                                                                                                              
                                                                                                              .pc-w620-tableCollapsed-1 > tbody,.pc-w620-tableCollapsed-1 > tbody > tr,.pc-w620-tableCollapsed-1 > tr {display: block !important;}
                                                                                                              .pc-w620-tableCollapsed-1.pc-width-fill > tbody,.pc-w620-tableCollapsed-1.pc-width-fill > tbody > tr,.pc-w620-tableCollapsed-1.pc-width-fill > tr {width: 100% !important;}
                                                                                                              .pc-w620-tableCollapsed-1.pc-w620-width-fill > tbody,.pc-w620-tableCollapsed-1.pc-w620-width-fill > tbody > tr,.pc-w620-tableCollapsed-1.pc-w620-width-fill > tr {width: 100% !important;}
                                                                                                              .pc-w620-tableCollapsed-1 > tbody > tr > td,.pc-w620-tableCollapsed-1 > tr > td {display: block !important;width: auto !important;}
                                                                                                              .pc-w620-tableCollapsed-1.pc-width-fill > tbody > tr > td,.pc-w620-tableCollapsed-1.pc-width-fill > tr > td {width: 100% !important;box-sizing: border-box !important;}
                                                                                                              .pc-w620-tableCollapsed-1.pc-w620-width-fill > tbody > tr > td,.pc-w620-tableCollapsed-1.pc-w620-width-fill > tr > td {width: 100% !important;box-sizing: border-box !important;}
                                                                                                              }
                                                                                                              @media (max-width: 520px) {
                                                                                                              .pc-w520-padding-25-30-0-30 {padding: 25px 30px 0px 30px !important;}
                                                                                                              .pc-w520-padding-15-30-0-30 {padding: 15px 30px 0px 30px !important;}
                                                                                                              .pc-w520-padding-15-25-15-25 {padding: 15px 25px 15px 25px !important;}
                                                                                                              .pc-w520-padding-10-30-10-30 {padding: 10px 30px 10px 30px !important;}
                                                                                                              }
                                                                                                              </style>
                                                                                                              <!--[if !mso]><!-- -->
                                                                                                              <style>
                                                                                                              @font-face { font-family: 'Montserrat'; font-style: normal; font-weight: 600; src: url('https://fonts.gstatic.com/s/montserrat/v29/JTUHjIg1_i6t8kCHKm4532VJOt5-QNFgpCu173w3aXw.woff') format('woff'), url('https://fonts.gstatic.com/s/montserrat/v29/JTUHjIg1_i6t8kCHKm4532VJOt5-QNFgpCu173w3aXo.woff2') format('woff2'); } @font-face { font-family: 'Montserrat'; font-style: normal; font-weight: 400; src: url('https://fonts.gstatic.com/s/montserrat/v29/JTUHjIg1_i6t8kCHKm4532VJOt5-QNFgpCtr6Hw3aXw.woff') format('woff'), url('https://fonts.gstatic.com/s/montserrat/v29/JTUHjIg1_i6t8kCHKm4532VJOt5-QNFgpCtr6Hw3aXo.woff2') format('woff2'); } @font-face { font-family: 'Montserrat'; font-style: normal; font-weight: 500; src: url('https://fonts.gstatic.com/s/montserrat/v29/JTUHjIg1_i6t8kCHKm4532VJOt5-QNFgpCtZ6Hw3aXw.woff') format('woff'), url('https://fonts.gstatic.com/s/montserrat/v29/JTUHjIg1_i6t8kCHKm4532VJOt5-QNFgpCtZ6Hw3aXo.woff2') format('woff2'); }
                                                                                                              </style>
                                                                                                              <!--<![endif]-->
                                                                                                              <!--[if mso]>
                                                                                                                 <style type="text/css">
                                                                                                                     .pc-font-alt {
                                                                                                                         font-family: Arial, Helvetica, sans-serif !important;
                                                                                                                     }
                                                                                                                 </style>
                                                                                                                 <![endif]-->
                                                                                                              <!--[if gte mso 9]>
                                                                                                                 <xml>
                                                                                                                     <o:OfficeDocumentSettings>
                                                                                                                         <o:AllowPNG/>
                                                                                                                         <o:PixelsPerInch>96</o:PixelsPerInch>
                                                                                                                     </o:OfficeDocumentSettings>
                                                                                                                 </xml>
                                                                                                                 <![endif]-->
                                                                                                             </head>

                                                                                                             <body class="body pc-font-alt" style="width: 100% !important; min-height: 100% !important; margin: 0 !important; padding: 0 !important; line-height: 1.5; font-weight: normal; color: #2D3A41; mso-line-height-rule: exactly; -webkit-font-smoothing: antialiased; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; font-variant-ligatures: normal; text-rendering: optimizeLegibility; -moz-osx-font-smoothing: grayscale; background-color: #ffffff;" bgcolor="#ffffff">
                                                                                                              <table class="pc-project-body" style="table-layout: fixed; width: 100%; min-width: 600px; background-color: #ffffff;" bgcolor="#ffffff" border="0" cellspacing="0" cellpadding="0" role="presentation">
                                                                                                               <tr>
                                                                                                                <td align="center" valign="top">
                                                                                                                 <table class="pc-project-container" align="center" style="width: 600px; max-width: 600px;" border="0" cellpadding="0" cellspacing="0" role="presentation">
                                                                                                                  <tr>
                                                                                                                   <td style="padding: 20px 0px 20px 0px;" align="left" valign="top">
                                                                                                                    <table border="0" cellpadding="0" cellspacing="0" role="presentation" width="100%">
                                                                                                                     <tr>
                                                                                                                      <td valign="top">
                                                                                                                       <table width="100%" border="0" cellspacing="0" cellpadding="0" role="presentation">
                                                                                                                        <tr>
                                                                                                                         <td valign="top" class="pc-w520-padding-25-30-0-30 pc-w620-padding-25-35-0-35" style="padding: 32px 40px 0px 40px; height: unset; background-color: #ffffff;" bgcolor="#ffffff">
                                                                                                                          <table border="0" cellpadding="0" cellspacing="0" role="presentation" width="100%">
                                                                                                                           <tr>
                                                                                                                            <td valign="top" align="left">
                                                                                                                             <div class="pc-font-alt" style="text-decoration: none;">
                                                                                                                              <div style="font-size: 24px;line-height: 42px;text-align:center;text-align-last:center;color:#434343;font-style:normal;font-weight:600;letter-spacing:-0.2px;">
                                                                                                                               <div><span style="font-family: 'Montserrat', Arial, Helvetica, sans-serif;">Reset Password</span>
                                                                                                                               </div>
                                                                                                                              </div>
                                                                                                                             </div>
                                                                                                                            </td>
                                                                                                                           </tr>
                                                                                                                          </table>
                                                                                                                         </td>
                                                                                                                        </tr>
                                                                                                                       </table>
                                                                                                                      </td>
                                                                                                                     </tr>
                                                                                                                     <tr>
                                                                                                                      <td valign="top">
                                                                                                                       <table width="100%" border="0" cellspacing="0" cellpadding="0" role="presentation">
                                                                                                                        <tr>
                                                                                                                         <td valign="top" class="pc-w520-padding-15-30-0-30 pc-w620-padding-15-35-0-35" style="padding: 24px 40px 16px 40px; height: unset; background-color: #ffffff;" bgcolor="#ffffff">
                                                                                                                          <table border="0" cellpadding="0" cellspacing="0" role="presentation" width="100%">
                                                                                                                           <tr>
                                                                                                                            <td valign="top" align="left">
                                                                                                                             <div class="pc-font-alt" style="text-decoration: none;">
                                                                                                                              <div style="font-size: 18px;line-height: 24px;text-align:center;text-align-last:center;color:#434343;font-style:normal;font-weight:500;letter-spacing:-0.2px;">
                                                                                                                               <div><span style="font-family: 'Montserrat', Arial, Helvetica, sans-serif;">We received a request to reset your password.To reset your password, click the button below:</span>
                                                                                                                               </div>
                                                                                                                              </div>
                                                                                                                             </div>
                                                                                                                            </td>
                                                                                                                           </tr>
                                                                                                                          </table>
                                                                                                                         </td>
                                                                                                                        </tr>
                                                                                                                       </table>
                                                                                                                      </td>
                                                                                                                     </tr>
                                                                                                                     <tr>
                                                                                                                      <td valign="top">
                                                                                                                       <table width="100%" border="0" cellspacing="0" cellpadding="0" role="presentation">
                                                                                                                        <tr>
                                                                                                                         <td valign="top" class="pc-w520-padding-15-25-15-25 pc-w620-padding-15-30-15-30" style="padding: 16px 40px 16px 40px; height: unset; background-color: #ffffff;" bgcolor="#ffffff">
                                                                                                                          <table width="100%" border="0" cellpadding="0" cellspacing="0" role="presentation">
                                                                                                                           <tr>
                                                                                                                            <th valign="top" align="center" style="text-align: center;">
                                                                                                                             <!--[if mso]>
                                                                                                                     <table border="0" cellpadding="0" cellspacing="0" role="presentation" align="center" style="border-collapse: separate; border-spacing: 0; margin-right: auto; margin-left: auto;">
                                                                                                                         <tr>
                                                                                                                             <td valign="middle" align="center" style="border-radius: 100px 100px 100px 100px; background-color: #1595e7; text-align:center; color: #ffffff; padding: 12px 32px 12px 32px; mso-padding-left-alt: 0; margin-left:32px;" bgcolor="#1595e7">
                                                                                                                                                 <a class="pc-font-alt" style="display: inline-block; text-decoration: none; font-family: 'Fira Sans', Arial, Helvetica, sans-serif; text-align: center;" href="{{0}}" target="_blank"><span style="font-size: 16px;line-height: 24px;color:#ffffff;letter-spacing:-0.2px;font-weight:500;font-style:normal;display:inline-block;"><span style="display:inline-block;"><span style="font-family: 'Montserrat', Arial, Helvetica, sans-serif;line-height: 150%;">Reset Password</span></span></span></a>
                                                                                                                                             </td>
                                                                                                                         </tr>
                                                                                                                     </table>
                                                                                                                     <![endif]-->
                                                                                                                             <!--[if !mso]><!-- -->
                                                                                                                             <a style="display: inline-block; box-sizing: border-box; border-radius: 100px 100px 100px 100px; background-color: #1595e7; padding: 12px 32px 12px 32px; font-family: 'Fira Sans', Arial, Helvetica, sans-serif; vertical-align: top; text-align: center; text-align-last: center; text-decoration: none; -webkit-text-size-adjust: none;" href="{{0}}" target="_blank"><span style="font-size: 16px;line-height: 24px;color:#ffffff;letter-spacing:-0.2px;font-weight:500;font-style:normal;display:inline-block;"><span style="display:inline-block;"><span style="font-family: 'Montserrat', Arial, Helvetica, sans-serif;line-height: 150%;">Activate Account</span></span></span></a>
                                                                                                                             <!--<![endif]-->
                                                                                                                            </th>
                                                                                                                           </tr>
                                                                                                                          </table>
                                                                                                                         </td>
                                                                                                                        </tr>
                                                                                                                       </table>
                                                                                                                      </td>
                                                                                                                     </tr>
                                                                                                                     <tr>
                                                                                                                      <td valign="top">
                                                                                                                       <table width="100%" border="0" cellspacing="0" cellpadding="0" role="presentation">
                                                                                                                        <tr>
                                                                                                                         <td valign="top" class="pc-w520-padding-10-30-10-30 pc-w620-padding-10-35-10-35" style="padding: 10px 40px 32px 40px; height: unset; background-color: #ffffff;" bgcolor="#ffffff">
                                                                                                                          <table border="0" cellpadding="0" cellspacing="0" role="presentation" width="100%">
                                                                                                                           <tr>
                                                                                                                            <td valign="top" align="left">
                                                                                                                             <div class="pc-font-alt" style="text-decoration: none;">
                                                                                                                              <div style="font-size: 15px;line-height: 21px;text-align:left;text-align-last:left;color:#333333;font-style:normal;font-weight:500;letter-spacing:-0.2px;">
                                                                                                                               <div><span style="font-family: 'Montserrat', Arial, Helvetica, sans-serif;">Best regards,</span>
                                                                                                                               </div>
                                                                                                                               <div><span style="font-family: 'Montserrat', Arial, Helvetica, sans-serif;">The Uwubankster Team</span>
                                                                                                                               </div>
                                                                                                                              </div>
                                                                                                                             </div>
                                                                                                                            </td>
                                                                                                                           </tr>
                                                                                                                          </table>
                                                                                                                         </td>
                                                                                                                        </tr>
                                                                                                                       </table>
                                                                                                                      </td>
                                                                                                                     </tr>
                                                                                                                    </table>
                                                                                                                   </td>
                                                                                                                  </tr>
                                                                                                                 </table>
                                                                                                                </td>
                                                                                                               </tr>
                                                                                                              </table>
                                                                                                             </body>

                                                                                                             </html>
                                                                                                             """
                                                                                                  }
                                                                 }
                                                             };

    public Email Find(EmailType type)
    {
        return m_Emails.TryGetValue(type, out var value) ? value : throw new ArgumentException($"Email of type {type} was not found.");
    }
}
