using System;

namespace MyStoreData.Enums;

public enum VerificationStatus
{
    Pending = 0,             // Blanc (En attente)
    OrderReceiptReceived = 1, // Bleu (OR Reçu)
    Paid = 2,                // Orange (Payés)
    CertificateReceived = 3  // Vert (Certificat reçu)
}