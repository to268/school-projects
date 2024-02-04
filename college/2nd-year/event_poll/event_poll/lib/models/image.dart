class Image {
  Image({required this.imageName});
  String imageName;
  Image.fromJson(Map<String, dynamic> json)
      : this(
          imageName: json['imageName'] as String,
        );
}
