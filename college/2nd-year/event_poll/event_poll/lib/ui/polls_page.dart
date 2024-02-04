import 'package:event_poll/states/polls_state.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';

import '../states/auth_state.dart';

class PollsPage extends StatefulWidget {
  const PollsPage({super.key});

  @override
  State<PollsPage> createState() => _PollsPageState();
}

class _PollsPageState extends State<PollsPage> {
  final dateFormater = DateFormat('dd/MM/yyyy', 'fr');
  late final width = MediaQuery.of(context).size.width;
  late final height = MediaQuery.of(context).size.height;

  @override
  void initState() {
    super.initState();
    context.read<PollsState>().getAll();
  }

  @override
  Widget build(BuildContext context) {
    var polls = context.watch<PollsState>().polls;
    if (polls == null) {
      return const Center(child: CircularProgressIndicator());
    }
    return Scaffold(
      body: ListView.builder(
        itemCount: polls.length,
        itemBuilder: (context, index) {
          return ListTile(
            title: Text(polls[index].name),
            subtitle: Text(polls[index].description),
            trailing: Text(dateFormater.format(polls[index].eventDate)),
            shape: Border(
                bottom: BorderSide(color: Theme.of(context).dividerColor)),
            onTap: () {
              Navigator.pushNamed(context, '/polls/detail',
                  arguments: context.read<PollsState>().polls?[index]);
            },
          );
        },
      ),
      floatingActionButton: SizedBox(
        width: width * 0.8,
        height: height * 0.1,
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: <Widget>[
            FloatingActionButton(
              onPressed: () {
                context.read<PollsState>().getAll();
              },
              child: const Icon(Icons.refresh),
            ),
            context.read<AuthState>().loggedIn &&
                    context.read<AuthState>().currentUser!.isAdmin
                ? FloatingActionButton(
                    heroTag: "create a poll",
                    onPressed: () {
                      Navigator.pushNamed(context, '/polls/create');
                    },
                    child: const Icon(Icons.add),
                  )
                : const SizedBox()
          ],
        ),
      ),
      floatingActionButtonLocation: FloatingActionButtonLocation.centerFloat,
    );
  }
}
