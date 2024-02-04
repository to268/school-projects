import 'dart:convert';
import 'dart:io';

import 'package:flutter/widgets.dart';

import '../configs.dart';
import 'package:http/http.dart' as http;

import '../models/poll.dart';
import '../result.dart';

class PollsState extends ChangeNotifier {
  List<Poll>? _polls;
  List<Poll>? get polls => _polls;
  String? _token;
  String? get token => _token;

  Future<List<Poll>?> getAll() async {
    final pollsResponse = await http.get(
      Uri.parse('${Configs.baseUrl}/polls'),
      headers: {HttpHeaders.contentTypeHeader: 'application/json'},
    );
    if (pollsResponse.statusCode == HttpStatus.ok) {
      _polls = (json.decode(pollsResponse.body) as List<dynamic>)
          .map((e) => Poll.fromJson(e))
          .toList();
      notifyListeners();
      return _polls;
    }
    notifyListeners();
    return null;
  }

  Future<Result<Unit, String>> createPoll(
      String name, String description, DateTime eventDate) async {
    final createResponse = await http.post(
      Uri.parse('${Configs.baseUrl}/polls'),
      headers: {
        HttpHeaders.contentTypeHeader: 'application/json',
        HttpHeaders.authorizationHeader: 'Bearer $_token',
      },
      body: json.encode({
        'name': name,
        'description': description,
        'eventDate': eventDate.toIso8601String(),
      }),
    );
    if (createResponse.statusCode == HttpStatus.created) {
      notifyListeners();
      return Result.success(unit);
    }
    notifyListeners();
    return Result.failure('Une erreur est survenue');
  }

  Future<Result<Unit, String>> updatePoll(
      int id, String name, String description, DateTime eventDate) async {
    final updateResponse = await http.put(
      Uri.parse('${Configs.baseUrl}/polls/$id'),
      headers: {
        HttpHeaders.contentTypeHeader: 'application/json',
        HttpHeaders.authorizationHeader: 'Bearer $_token',
      },
      body: json.encode({
        'name': name,
        'description': description,
        'eventDate': eventDate.toIso8601String(),
      }),
    );
    if (updateResponse.statusCode == HttpStatus.ok) {
      notifyListeners();
      return Result.success(unit);
    }
    notifyListeners();
    return Result.failure('Une erreur est survenue');
  }

  Future<Result<Unit, String>> deletePoll(int id) async {
    final updateResponse = await http.delete(
      Uri.parse('${Configs.baseUrl}/polls/$id'),
      headers: {
        HttpHeaders.contentTypeHeader: 'application/json',
        HttpHeaders.authorizationHeader: 'Bearer $_token',
      },
    );
    if (updateResponse.statusCode == HttpStatus.noContent) {
      notifyListeners();
      return Result.success(unit);
    }
    notifyListeners();
    return Result.failure('Une erreur est survenue');
  }

  Future<List<Poll>?> getImage(String name) async {
    final imageResponse = await http.get(
      Uri.parse('${Configs.baseUrl}/images/$name'),
      headers: {HttpHeaders.contentTypeHeader: 'application/json'},
    );
    if (imageResponse.statusCode == HttpStatus.ok) {
      return _polls;
    }
    notifyListeners();
    return null;
  }

  Future<Result<String, String>> updatePollImage(int id, File imageFile) async {
    final fileContent = await imageFile.readAsBytes();
    final request = http.MultipartRequest(
        'POST', Uri.parse('${Configs.baseUrl}/polls/$id/image'))
      ..headers.addAll({
        HttpHeaders.contentTypeHeader: 'application/json',
        HttpHeaders.authorizationHeader: 'Bearer $_token'
      })
      ..files.add(http.MultipartFile.fromBytes(
        '',
        fileContent,
        filename: imageFile.path.split('/').last,
      ));
    final response = await http.Response.fromStream(await request.send());
    if (response.statusCode == HttpStatus.ok) {
      final imageName = Poll.fromJson(json.decode(response.body)).imageName!;
      notifyListeners();
      return Result.success(imageName);
    } else {
      return Result.failure('Une erreur est survenue');
    }
  }

  setAuthToken(String? token) {
    _token = token;
  }
}
