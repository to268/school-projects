import 'dart:convert';
import 'dart:io';

import 'package:event_poll/models/user.dart';
import 'package:flutter/widgets.dart';

import '../configs.dart';
import 'package:http/http.dart' as http;

class AuthState extends ChangeNotifier {
  User? _currentUser;
  User? get currentUser => _currentUser;
  String? _token;
  String? get token => _token;
  bool get loggedIn => _currentUser != null;

  Future<User?> login(String username, String password) async {
    final loginResponse = await http.post(
      Uri.parse('${Configs.baseUrl}/auth/login'),
      headers: {HttpHeaders.contentTypeHeader: 'application/json'},
      body: json.encode({
        'username': username,
        'password': password,
      }),
    );
    if (loginResponse.statusCode == HttpStatus.ok) {
      _token = json.decode(loginResponse.body)['token'];
      print(_token);
      final userResponse = await http.get(
        Uri.parse('${Configs.baseUrl}/users/me'),
        headers: {
          HttpHeaders.authorizationHeader: 'Bearer $_token',
          HttpHeaders.contentTypeHeader: 'application/json',
        },
      );
      if (userResponse.statusCode == HttpStatus.ok) {
        _currentUser = User.fromJson(json.decode(userResponse.body));
        notifyListeners();
        return _currentUser;
      }
    }
    logout();
    return null;
  }

  Future<User?> signup(String username, String password) async {
    final signupResponse = await http.post(
      Uri.parse('${Configs.baseUrl}/auth/signup'),
      headers: {HttpHeaders.contentTypeHeader: 'application/json'},
      body: json.encode({
        'username': username,
        'password': password,
      }),
    );
    if (signupResponse.statusCode == HttpStatus.created) {
      _currentUser = await login(username, password);
      notifyListeners();
      return _currentUser;
    }

    logout();
    return null;
  }

  void logout() {
    _token = null;
    _currentUser = null;
    notifyListeners();
  }
}
