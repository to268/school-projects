import 'package:event_poll/states/polls_state.dart';
import 'package:flutter/material.dart';
import 'package:form_builder_file_picker/form_builder_file_picker.dart';
import 'package:provider/provider.dart';

class PollCreatePage extends StatefulWidget {
  const PollCreatePage({super.key});

  @override
  State<PollCreatePage> createState() => _PollCreatePageState();
}

class _PollCreatePageState extends State<PollCreatePage> {
  String nom = '';
  String description = '';
  DateTime date = DateTime.now();
  PlatformFile? image;
  String? error;
  final _formKey = GlobalKey<FormState>();
  String? _validateRequired(String? value) {
    return value == null || value.isEmpty ? 'Ce champ est obligatoire.' : null;
  }

  void _submit() async {
    if (!_formKey.currentState!.validate()) {
      return;
    }
    await context.read<PollsState>().createPoll(nom, description, date);
    if (context.mounted) {
      Navigator.pushNamedAndRemoveUntil(context, '/polls', (_) => false);
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
              onChanged: (value) => nom = value,
              validator: _validateRequired,
            ),
            const SizedBox(height: 16),
            TextFormField(
              decoration: const InputDecoration(labelText: 'Description'),
              onChanged: (value) => description = value,
              minLines: 1,
              maxLines: 5,
              validator: _validateRequired,
            ),
            const SizedBox(height: 16),
            InputDatePickerFormField(
              fieldLabelText: 'Date',
              firstDate: DateTime.now(),
              lastDate: DateTime.now().add(const Duration(days: 3650000)),
              errorFormatText: 'La date n\'est pas valide',
              errorInvalidText:
                  'La date de l\'évenement ne peut pas être dans le passé',
              onDateSubmitted: (value) => date = value,
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
