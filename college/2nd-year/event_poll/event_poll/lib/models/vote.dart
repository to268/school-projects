import 'package:event_poll/models/user.dart';

class Vote {
  Vote({
    required this.pollId,
    required this.status,
    required this.created,
    this.user,
  });
  int pollId;
  bool status;
  DateTime created;
  User? user;
  Vote.fromJson(Map<String, dynamic> json)
      : this(
          pollId: json['id'] as int,
          status: json['status'] as bool,
          created: DateTime.parse(json['eventDate']),
          user: User.fromJson(json['user']),
        );
}
