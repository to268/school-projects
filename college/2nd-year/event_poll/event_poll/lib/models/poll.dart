import 'package:event_poll/models/user.dart';

class Poll {
  Poll({
    required this.id,
    required this.name,
    required this.description,
    this.imageName,
    required this.eventDate,
    this.user,
  });
  int id;
  String name;
  String description;
  String? imageName;
  DateTime eventDate;
  User? user;
  Poll.fromJson(Map<String, dynamic> json)
      : this(
          id: json['id'] as int,
          name: json['name'] as String,
          description: json['description'] as String,
          imageName: json['imageName'] as String?,
          eventDate: DateTime.parse(json['eventDate']),
          user: User.fromJson(json['user']),
        );
}
