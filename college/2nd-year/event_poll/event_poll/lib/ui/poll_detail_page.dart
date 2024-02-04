import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import 'package:event_poll/models/poll.dart';
import 'package:provider/provider.dart';

import '../states/auth_state.dart';
import '../states/polls_state.dart';

class PollDetailPage extends StatefulWidget {
  const PollDetailPage({super.key});

  @override
  State<StatefulWidget> createState() => _PollDetailPageState();
}

class _PollDetailPageState extends State<PollDetailPage> {
  Future<void> deletePoll(
      BuildContext context, Poll poll, VoidCallback callback) async {
    await context.read<PollsState>().deletePoll(poll.id);
    callback();
  }

  @override
  Widget build(BuildContext context) {
    final Poll poll = ModalRoute.of(context)?.settings.arguments as Poll;
    final dateFormatter = DateFormat('dd/MM/yyyy', 'fr');
    String dateString = dateFormatter.format(poll.eventDate);
    return Scaffold(
      appBar: AppBar(
        automaticallyImplyLeading: false,
        title: Text(poll.name),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(poll.description),
            const SizedBox(height: 16.0),
            Text('Event date: $dateString'),
            const SizedBox(height: 16.0),
            Text('Created by: ${poll.user?.username}'),
            const SizedBox(height: 16.0),
            if (context.read<AuthState>().loggedIn &&
                context.read<AuthState>().currentUser!.isAdmin)
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  FloatingActionButton(
                    heroTag: "update a poll",
                    onPressed: () {
                      Navigator.pushNamed(context, '/polls/update',
                          arguments: poll);
                    },
                    child: const Icon(Icons.update),
                  ),
                  const SizedBox(width: 16.0),
                  FloatingActionButton(
                    heroTag: "delete a poll",
                    onPressed: () {
                      showDialog(
                        context: context,
                        builder: (_) => AlertDialog(
                          title: const Text('Confirm deletion'),
                          content: const Text(
                            'Are you sure you want to delete this poll?',
                          ),
                          actions: [
                            TextButton(
                              onPressed: () => Navigator.pop(context),
                              child: const Text('CANCEL'),
                            ),
                            TextButton(
                              onPressed: () async {
                                deletePoll(context, poll, () {
                                  Navigator.pop(context);
                                  Navigator.pop(context);
                                });
                              },
                              child: const Text('DELETE'),
                            ),
                          ],
                        ),
                      );
                    },
                    backgroundColor: Colors.red,
                    child: const Icon(Icons.delete),
                  ),
                ],
              )
            else
              const SizedBox(),
          ],
        ),
      ),
    );
  }
}
