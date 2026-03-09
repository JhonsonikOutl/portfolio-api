namespace Portfolio.Application.Helpers
{
    /// <summary>
    /// Helper para generar templates HTML para emails.
    /// </summary>
    public static class EmailTemplateHelper
    {
        public static string GenerateReplyTemplate(string recipientName, string replyBody, string senderName)
        {
            return $@"
<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Respuesta a tu mensaje</title>
</head>
<body style='margin: 0; padding: 0; font-family: Arial, sans-serif; background-color: #f3f4f6;'>
    <table role='presentation' style='width: 100%; border-collapse: collapse;'>
        <tr>
            <td style='padding: 40px 20px;'>
                <table role='presentation' style='max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0,0,0,0.1);'>
                    
                    <!-- Header -->
                    <tr>
                        <td style='background: linear-gradient(135deg, #6366F1 0%, #8B5CF6 100%); padding: 40px 30px; text-align: center;'>
                            <h1 style='margin: 0; color: #ffffff; font-size: 24px; font-weight: 700;'>
                                {senderName}
                            </h1>
                            <p style='margin: 8px 0 0 0; color: rgba(255,255,255,0.9); font-size: 14px;'>
                                Portfolio - Desarrollador Full Stack
                            </p>
                        </td>
                    </tr>

                    <!-- Greeting -->
                    <tr>
                        <td style='padding: 30px 30px 20px 30px;'>
                            <p style='margin: 0; font-size: 16px; color: #1f2937;'>
                                Hola <strong>{recipientName}</strong>,
                            </p>
                        </td>
                    </tr>

                    <!-- Body -->
                    <tr>
                        <td style='padding: 0 30px 30px 30px;'>
                            <div style='font-size: 15px; line-height: 1.6; color: #4b5563; white-space: pre-wrap;'>
                                {replyBody}
                            </div>
                        </td>
                    </tr>

                    <!-- Footer -->
                    <tr>
                        <td style='background-color: #f9fafb; padding: 30px; border-top: 1px solid #e5e7eb;'>
                            <p style='margin: 0 0 10px 0; font-size: 14px; color: #6b7280;'>
                                Saludos cordiales,<br>
                                <strong style='color: #1f2937;'>{senderName}</strong>
                            </p>
                            <p style='margin: 15px 0 0 0; font-size: 12px; color: #9ca3af;'>
                                Este correo es una respuesta a tu mensaje enviado desde mi portafolio.
                            </p>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
</body>
</html>";
        }

        public static string GenerateConfirmationTemplate(string recipientName, string subject, string radicate)
        {
            return $@"
<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Mensaje recibido</title>
</head>
<body style='margin: 0; padding: 0; font-family: Arial, sans-serif; background-color: #f3f4f6;'>
    <table role='presentation' style='width: 100%; border-collapse: collapse;'>
        <tr>
            <td style='padding: 40px 20px;'>
                <table role='presentation' style='max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0,0,0,0.1);'>

                    <!-- Header -->
                    <tr>
                        <td style='background: linear-gradient(135deg, #6366F1 0%, #8B5CF6 100%); padding: 40px 30px; text-align: center;'>
                            <h1 style='margin: 0; color: #ffffff; font-size: 24px; font-weight: 700;'>
                                Jonathan Aldana
                            </h1>
                            <p style='margin: 8px 0 0 0; color: rgba(255,255,255,0.9); font-size: 14px;'>
                                Portfolio - Desarrollador Full Stack
                            </p>
                        </td>
                    </tr>

                    <!-- Greeting -->
                    <tr>
                        <td style='padding: 30px 30px 10px 30px;'>
                            <p style='margin: 0; font-size: 16px; color: #1f2937;'>
                                Hola <strong>{recipientName}</strong>,
                            </p>
                        </td>
                    </tr>

                    <!-- Body -->
                    <tr>
                        <td style='padding: 10px 30px 30px 30px;'>
                            <p style='margin: 0 0 16px 0; font-size: 15px; line-height: 1.6; color: #4b5563;'>
                                He recibido tu mensaje con el asunto <strong>'{subject}'</strong> y me pondré en contacto contigo a la brevedad posible.
                            </p>

                            <p style='margin: 0 0 16px 0; font-size: 15px; line-height: 1.6; color: #4b5563;'>
                                Readicado generado:  <strong>'{radicate}'</strong> para seguimiento.
                            </p>

                            <p style='margin: 0; font-size: 15px; line-height: 1.6; color: #4b5563;'>
                                Gracias por comunicarte.
                            </p>
                        </td>
                    </tr>

                    <!-- Footer -->
                    <tr>
                        <td style='background-color: #f9fafb; padding: 30px; border-top: 1px solid #e5e7eb;'>
                            <p style='margin: 0 0 10px 0; font-size: 14px; color: #6b7280;'>
                                Saludos cordiales,<br>
                                <strong style='color: #1f2937;'>Jonathan Aldana</strong>
                            </p>
                            <p style='margin: 15px 0 0 0; font-size: 12px; color: #9ca3af;'>
                                Este correo es una confirmación automática. Por favor no respondas a este mensaje.
                            </p>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
</body>
</html>";
        }
    }
}
