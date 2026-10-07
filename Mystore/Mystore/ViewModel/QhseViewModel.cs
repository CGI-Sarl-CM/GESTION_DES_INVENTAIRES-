using CGIERPData;
using CGIERPData.Enums;
using CGIERPData.Models;
using CGIERPData.Models.EmbeddedObjects;
using CGIERPData.Models.QHSEModels;
using System;

namespace CGIERP.ViewModel;

public partial class QhseViewModel : BaseViewModel
{
    // Live Realm Queries: The UI will automatically animate and update when these change.
    public IQueryable<QhseAssignmentModel> ActiveAssignments { get; private set; }
    public IQueryable<QhseVerificationModel> Verifications { get; private set; }
    public IQueryable<ItemModel> AvailableQhseItems { get; private set; }

    public QhseViewModel(IRealmFactory realmFactory) : base(realmFactory)
    {
        Title = "QHSE Dashboard";
        InitializeLiveQueries();
    }

    private void InitializeLiveQueries()
    {
        var _realm = _realmFactory.GetRealmInstance();
        // Get items specifically belonging to QHSE that have stock > 0
        AvailableQhseItems = _realm.All<ItemModel>()
            .Where(i => i.Department.Name == "QHSE" && i.StockQuantity > 0)
            .OrderBy(i => i.Name);

        // Get assignments that haven't been returned yet
        ActiveAssignments = _realm.All<QhseAssignmentModel>()
            .Where(a => !a.IsReturned)
            .OrderByDescending(a => a.AssignmentDate);

        // Get verifications, ordering by the upcoming dates
        Verifications = _realm.All<QhseVerificationModel>()
            .OrderBy(v => v.NextVerificationDate);
    }

    #region Transactions - Assignments

    public async Task DispatchItem(ItemModel item, RecipientDetails recipient, int qty, string reason)
    {
        var _realm = _realmFactory.GetRealmInstance();
        if (item.StockQuantity < qty) return;

        // Write transactions are atomic
        await _realm.WriteAsync(() =>
        {
            item.StockQuantity -= qty;

            var assignment = new QhseAssignmentModel
            {
                Item = item,
                Recipient = recipient, // Embedded Object saved seamlessly
                QuantityAssigned = qty,
                Reason = reason
            };

            _realm.Add(assignment);
        });
    }

    public async Task MarkAssignmentReturned(QhseAssignmentModel assignment)
    {
        var _realm = _realmFactory.GetRealmInstance();
        await _realm.WriteAsync(() =>
        {
            assignment.IsReturned = true;
            assignment.ReturnDate = DateTimeOffset.UtcNow;

            // Restore stock
            assignment.Item.StockQuantity += assignment.QuantityAssigned;
        });
    }

    #endregion

    #region Transactions - Verifications

    public async Task AddNewVerification(string client, string designation, string serial, DateTimeOffset lastVerif, DateTimeOffset nextVerif, string agent, UserModel currentUser)
    {
        var _realm = _realmFactory.GetRealmInstance();
        await _realm.WriteAsync(() =>
        {
            var verification = new QhseVerificationModel
            {
                Client = client,
                Designation = designation,
                SerialNumberAndModel = serial,
                LastVerificationDate = lastVerif,
                NextVerificationDate = nextVerif,
                MincomAgentName = agent,
                CurrentStatusCode = (int)VerificationStatus.Pending
            };

            // Add the initial embedded audit log
            verification.StatusAuditTrail.Add(new StatusHistoryLog
            {
                StatusCode = (int)VerificationStatus.Pending,
                ChangedBy = currentUser
            });

            _realm.Add(verification);
        });
    }

    public async Task UpdateVerificationStatus(QhseVerificationModel verification, VerificationStatus newStatus, UserModel currentUser)
    {
        if (verification.CurrentStatusCode == (int)newStatus) return;

        var _realm = _realmFactory.GetRealmInstance();
        await _realm.WriteAsync(() =>
        {
            verification.CurrentStatusCode = (int)newStatus;

            // Push to the embedded audit trail. Perfect for ERP history tracking!
            verification.StatusAuditTrail.Add(new StatusHistoryLog
            {
                StatusCode = (int)newStatus,
                ChangedBy = currentUser
            });
        });
    }

    #endregion
}