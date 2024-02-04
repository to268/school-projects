import 'dart:convert';
import 'dart:io';

import 'package:event_poll/models/vote.dart';
import 'package:flutter/cupertino.dart';

import '../configs.dart';
import 'package:http/http.dart' as http;

import '../result.dart';

class VotesState extends ChangeNotifier {
  List<Vote>? _votes;
  List<Vote>? get votes => _votes;
  String? _token;
  String? get token => _token;

  Future<List<Vote>?> getAllVotes(int id) async {
    final votesResponse = await http.get(
      Uri.parse('${Configs.baseUrl}/$id/votes'),
      headers: {
        HttpHeaders.contentTypeHeader: 'application/json',
        HttpHeaders.authorizationHeader: 'Bearer $_token',
      },
    );
    if (votesResponse.statusCode == HttpStatus.ok) {
      _votes = (json.decode(votesResponse.body) as List<dynamic>)
          .map((e) => Vote.fromJson(e))
          .toList();
      notifyListeners();
      return _votes;
    }
    notifyListeners();
    return null;
  }

  Future<Result<Unit, String>> createUpdateVotes(int id, bool status) async {
    final createupdateResponse = await http.post(
      Uri.parse('${Configs.baseUrl}/$id/votes'),
      headers: {
        HttpHeaders.contentTypeHeader: 'application/json',
        HttpHeaders.authorizationHeader: 'Bearer $_token',
      },
      body: json.encode({
        'status': status,
      }),
    );
    if (createupdateResponse.statusCode == HttpStatus.created) {
      notifyListeners();
      return Result.success(unit);
    }
    notifyListeners();
    return Result.failure('Une erreur est survenue');
  }

  Future<Result<Unit, String>> deleteVote(int id) async {
    final updateResponse = await http.delete(
      Uri.parse('${Configs.baseUrl}/$id/votes'),
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

  setAuthToken(String? token) {
    _token = token;
  }
}
