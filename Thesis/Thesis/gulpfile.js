// Include gulp
var gulp = require('gulp');
 // Include plugins
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var rename = require('gulp-rename');
 // Concatenate JS Files
gulp.task('javascript', function() {
    return gulp.src(['bower_components/moment/moment.js','bower_components/bootstrap-material-datetimepicker/momentjs/min/moment-with-locales.min.js', 'bower_components/bootstrap-material-design/dist/js/*min.js'])
      .pipe(concat('bowercomponents.js'))
	  .pipe(rename({suffix: '.min'}))
      .pipe(uglify())
      .pipe(gulp.dest('Scripts'));
});

gulp.task('style', function() {
    return gulp.src(['bower_components/bootstrap-material-datetimepicker/css/*.css','bower_components/bootstrap-material-design/dist/css/*min.css'])
      .pipe(concat('bowercomponents.css'))
      .pipe(gulp.dest('Content'));
});


gulp.task('watch', function() {
   // Watch .js files
  gulp.watch('Scripts/bowercomponents.js', ['javascript']);
   // Watch .css files
  gulp.watch('Content/bowercomponents.css', ['style']);

 });



gulp.task('default', ['javascript','style','watch']);