import 'dart:io';

import 'package:flutter/material.dart';
import 'package:form_builder_file_picker/form_builder_file_picker.dart';
import 'package:provider/provider.dart';

import '../models/poll.dart';
import '../states/polls_state.dart';

class PollUpdatePage extends StatefulWidget {
  const PollUpdatePage({super.key});

  @override
  State<StatefulWidget> createState() => _PollUpdatePageState();
}

class _PollUpdatePageState extends State<PollUpdatePage> {
  late Poll poll;
  String nom = '';
  String description = '';
  DateTime date = DateTime.now();
  PlatformFile? image;
  String? error;
  final _formKey = GlobalKey<FormState>();

  String? _validateRequired(String? value) {
    return value == null || value.isEmpty ? 'Ce champ est obligatoire.' : null;
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    final route = ModalRoute.of(context);
    if (route != null) {
      poll = route.settings.arguments as Poll;
    }
  }

  void _submit() async {
    if (!_formKey.currentState!.validate()) {
      return;
    }
    context
        .read<PollsState>()
        .updatePoll(poll.id, poll.name, poll.description, poll.eventDate)
        .whenComplete(() => null);
    if (image != null) {
      context
          .read<PollsState>()
          .updatePollImage(poll.id, File(image!.path!))
          .whenComplete(() => null);
    }
    if (context.mounted) {
      Navigator.pushNamed(context, '/polls/detail', arguments: poll);
    } else {
      setState(() {
        error = 'Une erreur est survenue.';
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    return Container(
      margin: const EdgeInsets.all(32.0),
      child: Form(
        key: _formKey,
        child: Column(
          children: [
            TextFormField(
              decoration: const InputDecoration(labelText: 'Nom'),
              initialValue: poll.name,
              onChanged: (value) => poll.name = value,
              validator: _validateRequired,
            ),
            const SizedBox(height: 16),
            TextFormField(
              decoration: const InputDecoration(labelText: 'Description'),
              initialValue: poll.description,
              onChanged: (value) => poll.description = value,
              minLines: 1,
              maxLines: 5,
              validator: _validateRequired,
            ),
            const SizedBox(height: 16),
            InputDatePickerFormField(
              fieldLabelText: 'Date',
              initialDate: poll.eventDate.isBefore(DateTime.now())
                  ? DateTime.now()
                  : poll.eventDate,
              firstDate: DateTime.now(),
              lastDate: DateTime.now().add(const Duration(days: 3650000)),
              errorFormatText: 'La date n\'est pas valide',
              errorInvalidText:
                  'La date de l\'évenement ne peut pas être dans le passé',
              onDateSubmitted: (value) => poll.eventDate = value,
            ),
            const SizedBox(height: 16),
            FormBuilderFilePicker(
              name: 'image de couverture',
              decoration: const InputDecoration(labelText: "Attachments"),
              maxFiles: 1,
              previewImages: true,
              onChanged: (value) =>
                  value != null ? image = value.first : image = null,
              typeSelectors: [
                TypeSelector(
                  type: FileType.image,
                  selector: Row(
                    children: const <Widget>[
                      Icon(Icons.add_circle),
                      Padding(
                        padding: EdgeInsets.only(left: 8.0, top: 12.0),
                        child: Text("Add image"),
                      ),
                    ],
                  ),
                )
              ],
            ),
            const SizedBox(height: 16),
            if (error != null)
              Text(error!,
                  style: theme.textTheme.labelMedium!
                      .copyWith(color: theme.colorScheme.error)),
            ElevatedButton(
              onPressed: _submit,
              child: const Text('Enregister'),
            ),
          ],
        ),
      ),
    );
  }
}
