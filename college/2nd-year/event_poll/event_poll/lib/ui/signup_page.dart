import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../states/auth_state.dart';

class SignupPage extends StatefulWidget {
  const SignupPage({super.key});

  @override
  State<SignupPage> createState() => _SignupPageState();
}

class _SignupPageState extends State<SignupPage> {
  String username = '';
  String password = '';
  String confirmPassword = '';
  String? error;
  final _formKey = GlobalKey<FormState>();

  String? _validateRequired(String? value) {
    return value == null || value.isEmpty ? 'Ce champ est obligatoire.' : null;
  }

  String? _validatePassword(String? value) {
    if (value == null || value.isEmpty) {
      return 'Ce champ est obligatoire.';
    } else if (value.length < 8) {
      return 'Le mot de passe doit comporter au moins 8 caractères.';
    } else if (value != confirmPassword) {
      return 'Les mots de passe ne correspondent pas.';
    } else {
      return null;
    }
  }

  void _submit() async {
    if (!_formKey.currentState!.validate()) {
      return;
    }
    final user = await context.read<AuthState>().signup(username, password);
    if (user != null) {
      if (context.mounted) {
        Navigator.pushNamedAndRemoveUntil(context, '/polls', (_) => false);
      }
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
              decoration: const InputDecoration(labelText: 'Identifiant'),
              onChanged: (value) => username = value,
              validator: _validateRequired,
            ),
            const SizedBox(height: 16),
            TextFormField(
              decoration: const InputDecoration(labelText: 'Mot de passe'),
              obscureText: true,
              onChanged: (value) => password = value,
              validator: _validatePassword,
            ),
            const SizedBox(height: 16),
            TextFormField(
              decoration:
              const InputDecoration(labelText: 'Confirmer le mot de passe'),
              obscureText: true,
              onChanged: (value) => confirmPassword = value,
              validator: _validatePassword,
            ),
            const SizedBox(height: 16),
            if (error != null)
            Text(error!,
              style: theme.textTheme.labelMedium!
              .copyWith(color: theme.colorScheme.error)),
            ElevatedButton(
              onPressed: _submit,
              child: const Text('Inscription'),
            ),
          ],
        ),
      ),
    );
  }
}
