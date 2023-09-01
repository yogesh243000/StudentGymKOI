SELECT U.Id, M.MembershipID, M.MemberShipName, U.Email
FROM AspNetUsers AS U
JOIN UserMembership AS UM ON U.Id = UM.UserID
JOIN Membership AS M ON UM.MembershipID = M.MembershipID
WHERE U.Id = '292564b6-3956-4240-b295-40e19105e8bf';
